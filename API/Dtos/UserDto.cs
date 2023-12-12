namespace API.Dtos
{
    public class UserDto
    {
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string Token { get; set; }
        public string Role{get;set;}

    }
}




// namespace API
// {
//     public class Program
//     {
//         public static async Task Main(string[] args)
//         {
//              var listDataEntry=new List<AttendanceLog>();
//                 CultureInfo culture = new CultureInfo("en-US"); 
//                 listDataEntry.Add(new AttendanceLog{
//                 EmployeeId="",
//                 CheckInDateTime=Convert.ToDateTime("10/22/2015 12:10:15 PM", culture),
//                 CheckOutDateTime=Convert.ToDateTime("10/22/2015 12:09:15 PM", culture)
//                 });
//                 listDataEntry.Add(new AttendanceLog{
//                 EmployeeId="",
//                 CheckInDateTime=Convert.ToDateTime("10/22/2015 12:10:15 PM", culture),
//                 CheckOutDateTime=Convert.ToDateTime("10/22/2015 12:08:15 PM", culture)
//                 });
//                 listDataEntry.Add(new AttendanceLog{
//                 EmployeeId="",
//                 CheckInDateTime=Convert.ToDateTime("10/22/2015 12:10:15 PM", culture),
//                 CheckOutDateTime=Convert.ToDateTime("10/22/2015 12:2:15 PM", culture)
//                 });
//             var result=CalculateMissedMinutes(listDataEntry);    
//         }
//        public List<AttendanceMissedMinutes> CalculateMissedMinutes(List<AttendanceLog> model)
//         {
//                 var missedList=new List<AttendanceMissedMinutes>();
//                 var tottalMinutes=0;
//                 foreach (var item in model)
//                 {
//                     tottalMinutes=0;
//                     for (int i = 0; i < 7; i++)
//                     {
//                         if(item.CheckInDateTime.DayOfWeek != 'Saturday')
//                         {
//                             if(item.CheckInDateTime.Hour == 2 && item.CheckInDateTime.Minute > 30)
//                             { 
//                                 int minute=item.CheckInDateTime.Minute-30;
//                                 tottalMinutes = tottalMinutes + minute;
//                             }
//                             if(item.CheckInDateTime.Hour > 2){
//                                 int hour=item.CheckInDateTime.Hour-2;
//                                 int minute=item.CheckInDateTime.Minute;
//                                 if(hour != 0)
//                                 {
//                                     tottalMinutes=tottalMinutes+((hour*60)-minute);
//                                 }    
//                             }
//                             if(item.CheckOutDateTime.Hour == 6 && item.CheckOutDateTime.Minute < 30)
//                             {
//                                 int minute=30-item.CheckOutDateTime.Minute;
//                                 tottalMinutes=tottalMinutes+minute;
//                             }  
//                             if(item.CheckOutDateTime.Hour < 6) 
//                             {
//                                 int hour=6-item.CheckOutDateTime.Hour;
//                                 int minute=item.CheckOutDateTime.Minute;
//                                 if(hour!=0)
//                                 {
//                                     tottalMinutes=tottalMinutes+((hour*60)-minute);
//                                 }
//                             }                       
//                         }
//                         else if(item.CheckInDateTime.DayOfWeek != 'Sunday'){
//                             contniue;
//                         }
//                         else
//                         {
//                             if(item.CheckInDateTime.Hour == 2 && item.CheckInDateTime.Minute > 30)
//                             {                               
//                                 int minute=item.CheckInDateTime.Minute-30;
//                                 tottalMinutes = tottalMinutes + minute;
//                             }
//                             if(item.CheckInDateTime.Hour > 2)
//                             {
//                                 int hour=item.CheckInDateTime.Hour-2;
//                                 int minute=item.CheckInDateTime.Minute;
//                                 if(hour != 0)
//                                 {
//                                     tottalMinutes=tottalMinutes+((hour*60)-minute);
//                                 }    
//                             }
//                             if(item.CheckOutDateTime.Hour == 11 && item.CheckOutDateTime.Minute < 30)
//                             {
//                                 int minute=30-item.CheckOutDateTime.Minute;
//                                 tottalMinutes=tottalMinutes+minute;
//                             }  
//                             if(item.CheckOutDateTime.Hour < 11) 
//                             {
//                                 int hour=11-item.CheckOutDateTime.Hour;
//                                 int minute=item.CheckOutDateTime.Minute;

//                                 if(hour!=0)
//                                 {
//                                     tottalMinutes=tottalMinutes+((hour*60)-minute);
//                                 }
//                             } 
//                         }
//                     }
//                 var singleList=new AttendanceMissedMinutes();
//                 singleList.EmployeeId=model.EmployeeId;
//                 singleList.MissedMinutes=tottalMinutes;

//                 missedList.Add(singleList);
//                 }

//               return missedList;
//         }
//     }
//     public class AttendanceLog
//     {
//         public Guid EmployeeId{ get; set; }
//         public DateTime CheckInDateTime{ get; set; }
//         public DateTime CheckOutDateTime{ get; set; }
//     }
//     public class AttendanceMissedMinutes
//     {
//         public Guid EmployeeId{ get; set; }
//         public Double MissedMinutes{get;set;}
//     }
// }


  

