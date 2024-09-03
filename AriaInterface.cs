using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;

namespace TidsBok
{
    public static class AriaInterface
    {
        private static SqlConnection connection = null;



        public static void Connect()
        {
            connection = new SqlConnection("data source = SLTVARDB3; initial catalog = VARIAN; persist security info = True; user id = AriaReader; password = ecivres; MultipleActiveResultSets = True");
            
            connection.Open();
        }

        public static void Disconnect()
        {
            connection.Close();
        }

        public static DataTable Query(string queryString)
        {
            DataTable dataTable = new DataTable();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(queryString, connection) { MissingMappingAction = MissingMappingAction.Passthrough, MissingSchemaAction = MissingSchemaAction.Add };
                adapter.Fill(dataTable);
                adapter.Dispose();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dataTable;
        }

        public static DataTable GetScheduledActivities(string startDate, string endDate, string machine)
        {
            Connect();
            DataTable datatable = AriaInterface.Query(@"SELECT DISTINCT
                                                            Patient.PatientId,
                                                            ScheduledActivity.ScheduledStartTime
                                                        FROM
                                                            Machine,
                                                            ScheduledActivity,
                                                            ResourceActivity,
                                                            Activity,
                                                            ActivityInstance,
                                                            Patient
                                                        WHERE
                                                            (Patient.PatientId like '19%' OR
                                                            Patient.PatientId like '20%') AND
                                                            ScheduledActivity.PatientSer = Patient.PatientSer AND
                                                            ScheduledActivity.ScheduledStartTime BETWEEN '" + startDate + @"' AND '" + endDate + @"' AND
                                                            ScheduledActivity.ScheduledActivitySer = ResourceActivity.ScheduledActivitySer AND
                                                            ResourceActivity.ResourceSer = Machine.ResourceSer AND
                                                            Machine.MachineName LIKE '" + machine + @"' AND
                                                            ActivityInstance.ActivityInstanceSer = ScheduledActivity.ActivityInstanceSer AND
                                                            Activity.ActivitySer = ActivityInstance.ActivitySer AND
                                                            Activity.ActivityCode != 'Kortkontroll TB1' AND
                                                            Activity.ActivityCode != 'Kortkontroll TB2' AND
                                                            Activity.ActivityCode != 'Paus' AND
                                                            Activity.ActivityCode != 'RESERVERAT' AND
                                                            Activity.ActivityCode != 'QA'
                                                        ORDER BY
                                                            ScheduledActivity.ScheduledStartTime
                                                            ");
            Disconnect();
            return datatable;
        }
        public static DataTable GetScheduledActivitiesAlreadyTreated(string startDate, string endDate)
        {
            Connect();
            DataTable datatable = AriaInterface.Query(@"SELECT DISTINCT
                                                            Patient.PatientId,
                                                            ScheduledActivity.ScheduledStartTime
                                                        FROM
                                                            Machine,
                                                            ScheduledActivity,
                                                            ResourceActivity,
                                                            Activity,
                                                            ActivityInstance,
                                                            Patient
                                                        WHERE
                                                            (Patient.PatientId like '19%' OR
                                                            Patient.PatientId like '20%') AND
                                                            ScheduledActivity.PatientSer = Patient.PatientSer AND
                                                            ScheduledActivity.ScheduledStartTime BETWEEN '" + startDate + @"' AND '" + endDate + @"' AND
                                                            ScheduledActivity.ScheduledActivitySer = ResourceActivity.ScheduledActivitySer AND
                                                            ResourceActivity.ResourceSer = Machine.ResourceSer AND
                                                            (Machine.MachineName LIKE 'Strålbehandling 1' OR
                                                            Machine.MachineName LIKE 'Strålbehandling 2') AND
                                                            ActivityInstance.ActivityInstanceSer = ScheduledActivity.ActivityInstanceSer AND
                                                            Activity.ActivitySer = ActivityInstance.ActivitySer AND
                                                            Activity.ActivityCode != 'Kortkontroll TB1' AND
                                                            Activity.ActivityCode != 'Kortkontroll TB2' AND
                                                            Activity.ActivityCode != 'Paus' AND
                                                            Activity.ActivityCode != 'RESERVERAT' AND
                                                            Activity.ActivityCode != 'QA'
                                                        ORDER BY
                                                            ScheduledActivity.ScheduledStartTime
                                                            ");
            Disconnect();
            return datatable;
        }
        public static DataTable GetPatientList(string startDate, string endDate, string machine)
        {
            Connect();
            DataTable datatable = AriaInterface.Query(@"SELECT DISTINCT
                                                            Patient.PatientId
                                                        FROM
                                                            Machine,
                                                            ScheduledActivity,
                                                            ResourceActivity,
                                                            Activity,
                                                            ActivityInstance,
                                                            Patient
                                                        WHERE
                                                            (Patient.PatientId like '19%' OR
                                                            Patient.PatientId like '20%') AND
                                                            ScheduledActivity.PatientSer = Patient.PatientSer AND
                                                            ScheduledActivity.ScheduledStartTime BETWEEN '" + startDate + @"' AND '" + endDate + @"' AND
                                                            ScheduledActivity.ScheduledActivitySer = ResourceActivity.ScheduledActivitySer AND
                                                            ResourceActivity.ResourceSer = Machine.ResourceSer AND
                                                            Machine.MachineName LIKE '" + machine + @"' AND
                                                            ActivityInstance.ActivityInstanceSer = ScheduledActivity.ActivityInstanceSer AND
                                                            Activity.ActivitySer = ActivityInstance.ActivitySer AND
                                                            Activity.ActivityCode != 'Kortkontroll TB1' AND
                                                            Activity.ActivityCode != 'Kortkontroll TB2' AND
                                                            Activity.ActivityCode != 'RESERVERAT' AND
                                                            Activity.ActivityCode != 'QA'
                                                        ORDER BY
                                                            Patient.PatientId
                                                            ");
            Disconnect();
            return datatable;
        }
    }
}