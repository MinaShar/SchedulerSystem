using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PeriodInfoToAdmin
    {
        public string Type { get; set; }
        public int LectureID { get; set; }
        public List<Doctor> Doctors { get; set; }
        public string Place { get; set; }
        public int Capacity { get; set; }
        public int SectionNumber { get; set; }


        public static PeriodInfoToAdmin GetPeriodInfo(int CourseSemesterID,int GroupNumber,int PeriodDay,int PeriodNumber)
        {
            LecturesOfGroups LectureRequired = LecturesOfGroups.GetLectureOfPeriod(CourseSemesterID, GroupNumber, PeriodDay, PeriodNumber);
            if ( LectureRequired != null)
            {
                string PeriodType;
                if (LectureRequired.PeriodType == 1)
                {
                    PeriodType = "Lecture";
                }else if (LectureRequired.PeriodType == 2)
                {
                    PeriodType = "Section";
                }
                else
                {
                    PeriodType = "Lab";
                }
                List<Doctor> DoctorsOfPeriod = LecturesOfGroupsDoctors.GetDoctorsOfLecturePeriod(LectureRequired.Id);
                return new PeriodInfoToAdmin
                {
                    Type = PeriodType,
                    LectureID = LectureRequired.Id,
                    Doctors = DoctorsOfPeriod,
                    Place = LectureRequired.Place,
                    Capacity=LectureRequired.Capacity,
                    SectionNumber=LectureRequired.SectionNumber
                };
            }
            else
            {
                return null;
            }
        }

    }
}
