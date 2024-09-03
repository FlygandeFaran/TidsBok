using MouseClick;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MouseClick.MouseOperations;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using System.Windows.Input;
using System.Drawing;
using System.Runtime.InteropServices.ComTypes;
using System.IO;

namespace TidsBok
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dialog dialog = new Dialog();
            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                Run(dialog.Machine, dialog.StartDate, dialog.EndDate, dialog.CheckedPatients);
            }
        }
        private static void Run(string machine, DateTime start, DateTime end, List<string> CheckedPatients)
        {
            end = end.AddDays(1);
            var dates = new List<DateTime>();
            for (var dt = start; dt < end; dt = dt.AddDays(1))
            {
                dates.Add(dt);
            }

            bool isFirst = true;

            DataTable appointmentListAlreadyTreated = AriaInterface.GetScheduledActivitiesAlreadyTreated(start.AddDays(-14).ToString(), start.ToString()); // Skapar lista som tar alla patienter som har fått behandling de senaste 14 dagarna

            for (int j = 0; j < dates.Count; j++)
            {
                start = dates[j];
                end = start.AddDays(1);
                DataTable appointmentList = AriaInterface.GetScheduledActivities(start.ToString(), end.ToString(), machine);

                appointmentList = RemoveRowsWithFirstItemMatch(appointmentList, CheckedPatients); //tar bort de som inte är icheckade

                appointmentList = RemoveFirstFractionPatients(appointmentList, appointmentListAlreadyTreated, out appointmentListAlreadyTreated);
                for (int i = 0; i < appointmentList.Rows.Count; i++)
                {
                    string patID = appointmentList.Rows[i].ItemArray[0].ToString();
                    DateTime scheduledStart = Convert.ToDateTime(appointmentList.Rows[i].ItemArray[1].ToString());
                    if (i == 1)
                        isFirst = false;

                    MousePoint scheduledStartPosition = CalculateTimeSlotPosition(scheduledStart, j);
                    SchedulePatient(scheduledStartPosition, patID, isFirst);
                }
            }

            Done();

        }
        private static DataTable RemoveFirstFractionPatients(DataTable appointmentList, DataTable alreadyTreated, out DataTable AlreadyTreatedUpdated) //Kollar om icheckade patienter har fått behandling the senaste 14 dagarna
        {
            List<DataRow> toBeRemoved = new List<DataRow>();
            foreach (DataRow appointmentRow in appointmentList.Rows)
            {
                bool isTreated = false;
                foreach (DataRow alreadyTreatedRow in alreadyTreated.Rows)
                {
                    if (alreadyTreatedRow.ItemArray[0].Equals(appointmentRow.ItemArray[0].ToString()))// Jämför patienter som har fått behandling de senaste 14 dagarna mot dagens lista över patienter
                    {
                        isTreated = true;
                    }
                }
                if (!isTreated)
                {
                    alreadyTreated.Rows.Add(appointmentRow.ItemArray);
                    toBeRemoved.Add(appointmentRow);
                }
            }
            foreach (DataRow appointmentRow in toBeRemoved)
            {
                appointmentList.Rows.Remove(appointmentRow);
            }
            AlreadyTreatedUpdated = alreadyTreated;

            return appointmentList;
        }
        private static DataTable RemoveRowsWithFirstItemMatch(DataTable PatientList, List<string> CheckedPatients)
        {
            // Use LINQ to filter out rows based on the condition

            var rowsToKeep = PatientList.AsEnumerable()
            .Where(row => CheckedPatients.Contains(row.ItemArray[0]?.ToString()))
            .ToList();

            var rowsToRemove = PatientList.Rows.Cast<DataRow>().Except(rowsToKeep).ToList();
            // Remove rows that do not match the condition
            foreach (DataRow row in rowsToRemove)
            {
                PatientList.Rows.Remove(row);
            }
            return PatientList;
        }

        private static void Done()
        {
            using (Form form = new Form())
            {
                Bitmap img = new Bitmap(@"\\ltvastmanland.se\ltv\shares\rhosonk\Strålbehandling\VARIAN\Eclipse Script\Bilder\Glad gubbe litet format.jpg");

                form.StartPosition = FormStartPosition.CenterScreen;
                form.Size = img.Size;

                PictureBox pb = new PictureBox();
                pb.Dock = DockStyle.Fill;
                pb.Image = img;

                form.Controls.Add(pb);
                form.ShowDialog();
            }
        }

        private static MousePoint CalculateTimeSlotPosition(DateTime scheduledStart, int dag)
        {
            double noOfQuarterHours = scheduledStart.Minute / 15;
            double noOfHours = scheduledStart.Hour - 7;
            double n = noOfHours * 4 + noOfQuarterHours;

            int y = (int)(235 + 15 * n);
            int x = 360 + 385 * (dag);

            MousePoint scheduledStartPosition = new MousePoint(x, y);
            return scheduledStartPosition;
        }
        private static void SchedulePatient(MousePoint scheduledStartPosition, string patID, bool isFirst)
        {
            string filepath = @"\\ltvastmanland.se\ltv\shares\rhosonk\Strålbehandling\VARIAN\Eclipse Script\Settings\PositionCosmic.txt";
            int BehPosition = int.Parse(File.ReadAllLines(filepath)[0]);
            int buffert = 500;

            patID = patID.Substring(2);

            Thread.Sleep(1500);
            MousePoint mp = new MousePoint(250, 50);
            SetCursorPosition(mp);
            Thread.Sleep(100);
            CheckIfMoved(mp);
            MouseClick();
            Thread.Sleep(300);
            SendKeys.SendWait(patID);
            SendKeys.SendWait("{ENTER}");

            //Öppnar bokning
            SetCursorPosition(scheduledStartPosition);
            Thread.Sleep(1000);
            CheckIfMoved(scheduledStartPosition);
            MouseClick();
            SendKeys.SendWait("^+{B}");
            if (isFirst)
                Thread.Sleep(5000);
            else
                Thread.Sleep(3000);

            //Vårdtjänst 
            mp = new MousePoint(606, 600);
            SetCursorPosition(mp);
            MouseClick();
            mp = new MousePoint(606, BehPosition); //833 för daglig, 737 för nystart
            SetCursorPosition(mp);
            Thread.Sleep(100);
            MouseClick();
            Thread.Sleep(100);
            CheckIfMoved(mp);

            //Kontakttyp
            mp = new MousePoint(830, 600);
            SetCursorPosition(mp);
            MouseClick();
            mp = new MousePoint(830, 675);
            SetCursorPosition(mp);
            Thread.Sleep(100);
            MouseClick();
            Thread.Sleep(100);
            CheckIfMoved(mp);

            //Besökstyp
            mp = new MousePoint(606, 727);
            SetCursorPosition(mp);
            MouseClick();
            mp = new MousePoint(606, 797);
            SetCursorPosition(mp);
            Thread.Sleep(100);
            MouseClick();
            Thread.Sleep(100);
            CheckIfMoved(mp);

            //PatientAvgift
            mp = new MousePoint(1180, 655);
            SetCursorPosition(mp);
            MouseClick();
            mp = new MousePoint(1180, 725);
            SetCursorPosition(mp);
            Thread.Sleep(100);
            MouseClick();
            Thread.Sleep(100);
            CheckIfMoved(mp);

            //Spara och boka
            mp = new MousePoint(1345, 900);
            SetCursorPosition(mp);
            Thread.Sleep(100);
            MouseClick();
            Thread.Sleep(2000);
            CheckIfMoved(mp);

            //Vid dubbelbokning
            mp = new MousePoint(934, 700);
            SetCursorPosition(mp);
            Thread.Sleep(100);
            MouseClick();
            mp = new MousePoint(930, 560);
            SetCursorPosition(mp);
            Thread.Sleep(300);
            MouseClick();
            Thread.Sleep(2500 + buffert);
            CheckIfMoved(mp);

            //Rensa patient
            SendKeys.SendWait("^{F11}");
            Thread.Sleep(1000);
            CheckIfMoved(mp);


        }
        private static void MouseClick()
        {
            MouseEvent(MouseEventFlags.LeftDown);
            MouseEvent(MouseEventFlags.LeftUp);
        }
        private static void CheckIfMoved(MousePoint mp)
        {
            MousePoint currentMp = GetCursorPosition();
            if (currentMp.X != mp.X || currentMp.Y != mp.Y)
            {
                Interruption interruption = new Interruption();
                DialogResult result = interruption.ShowDialog();
                if (result == DialogResult.Yes)
                {
                    SetCursorPosition(mp);
                }
                else
                    Environment.Exit(0);
            }
        }
    }
}
