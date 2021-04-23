using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Models;

namespace SSP
{
    class ConnectedStudents
    {
        public int StudentID { get; set; }
        public string ConnectionID { get; set; }
    }

    public class MyHub1 : Hub
    {
        private static List<ConnectedStudents> ConnecStudents = new List<ConnectedStudents>();

        public void Alert(int x)
        {
            Clients.Caller.alert(x);
        }
        public void AlertNewChangeInPeriod(int LectureOfgroupID)
        {
            Clients.Others.AlertNewChangeInPeriod(LectureOfgroupID);
        }

        public void StudentArrived(int StudentID)
        {
            ConnecStudents.Add(new ConnectedStudents { StudentID = StudentID, ConnectionID = Context.ConnectionId });
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            if (ConnecStudents.Any(x => x.ConnectionID == Context.ConnectionId))
            {
                ConnectedStudents st = ConnecStudents.First(x => x.ConnectionID == Context.ConnectionId);
                StudentCourseSemester.DeleteUnsavedRegistration(st.StudentID);
                ConnecStudents.Remove(st);
            }
            return base.OnDisconnected(stopCalled);
        }
    }
}