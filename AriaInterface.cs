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
            //string filename = @"\\ltvastmanland.se\ltv\shares\rhosonk\Strålbehandling\VARIAN\Eclipse Script\Settings\aria_account_information.txt";
            //StreamReader sr = new StreamReader(filename, false);
            //string connectionStr = sr.ReadLine();
            //sr.Close();
            // Clinical database
            //connection = new SqlConnection("data source = SLTVARDB3; initial catalog = VARIAN; persist security info = True; user id = miqareader ; password = Tratti77Uh!; MultipleActiveResultSets = True");
            connection = new SqlConnection("data source = SLTVARDB3; initial catalog = VARIAN; persist security info = True; user id = AriaReader; password = ecivres; MultipleActiveResultSets = True");
            // Clinical database
            //connection = new SqlConnection("data source = SLTVARDB3; initial catalog = VARIAN; persist security info = True; MultipleActiveResultSets = True; Integrated Security = SSPI");

            // Research database
            //connection = new SqlConnection("data source = MCLA31708; initial catalog = VARIAN; persist security info = True; MultipleActiveResultSets = True; Integrated Security = SSPI");

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
                                                            Activity.ActivityCode != 'QA'
                                                        ORDER BY
                                                            Patient.PatientId
                                                            ");
            Disconnect();
            return datatable;
        }
        public static DataTable GetPatientWithFirstFraction(string startDate, string endDate, string machine)
        {
            Connect();
            DataTable datatable = AriaInterface.Query(@"SELECT DISTINCT
                                                            Patient.PatientId,
                                                            RadiationHstry.FractionNumber
                                                        FROM
                                                            Machine,
                                                            ScheduledActivity,
                                                            ResourceActivity,
                                                            Activity,
                                                            ActivityInstance,
                                                            RadiationHstry,
                                                            TreatmentRecord,
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
                                                            TreatmentRecord.PatientSer=Patient.PatientSer AND
                                                            RadiationHstry.TreatmentRecordSer=TreatmentRecord.TreatmentRecordSer AND
                                                            RadiationHstry.TreatmentEndTime BETWEEN '" + startDate + @"' AND '" + endDate + @"'
                                                        ORDER BY
                                                            ScheduledActivity.ScheduledStartTime
                                                            ");
            Disconnect();
            return datatable;
        }
        public static DataTable GetMachine()
        {
            Connect();
            DataTable datatable = AriaInterface.Query(@"SELECT
                                                            *
                                                        FROM 
                                                            Resource,
                                                            Machine
                                                        WHERE
                                                            Resource.ResourceSer = Machine.ResourceSer AND
                                                            Machine.MachineName LIKE 'Strålbehandling 2'
                                                            ");
            Disconnect();
            return datatable;
        }
        public static DataTable GetCommonColumn()
        {
            Connect();
            DataTable datatable = AriaInterface.Query(@"
SELECT      c.name  AS 'ColumnName'
            ,(SCHEMA_NAME(t.schema_id) + '.' + t.name) AS 'TableName'
FROM        sys.columns c
JOIN        sys.tables  t   ON c.object_id = t.object_id
WHERE       c.name LIKE '%Fraction%'
ORDER BY    TableName
            ,ColumnName;
                                                            ");
            Disconnect();
            return datatable;
        }
    }
}