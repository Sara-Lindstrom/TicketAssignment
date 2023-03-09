using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TicketAssignment.Models.Entities;

namespace TicketAssignment.Services
{
    internal class TicketService
    {

        public string SetTimeRemaining(DateTime CreatedTime, SeverityEnum severity)
        {
            DateTime _deadline;
            switch (severity)
            {
                case SeverityEnum.Minor:
                    _deadline = CreatedTime.AddHours(12);
                    break;
                case SeverityEnum.Important:
                    _deadline = CreatedTime.AddHours(5);
                    break;
                case SeverityEnum.Urgent:
                    _deadline = CreatedTime.AddHours(1);
                    break;
                default:
                    throw new InvalidOperationException("Unknown severity");
            }

            TimeSpan remaining = _deadline - DateTime.Now;

            if (remaining.TotalMinutes <= 0)
            {
                return "Overdue";
            }
            else if (remaining.TotalHours >= 1)
            {
                return $"{(int)remaining.TotalHours} hours left";
            }
            else if (remaining.TotalMinutes <= 60)
            {
                return $"{(int)remaining.TotalMinutes} minutes left";
            }
            else
            {
                return "Less than a minute left";
            }
        }

        //https://www.abstractapi.com/guides/format-phone-number-c#:~:text=One%20way%20to%20format%20a,string%20with%20the%20specified%20format.
        public string FormatPhoneNumber(string _phonenumber)
        {
            string PhoneNumber = "";
            string numbers = "0123456789";
            int i = 0;

            for (i = 0; i < _phonenumber.Length; i++)
            {
                if (numbers.Contains(_phonenumber.ElementAt(i)))
                {
                    PhoneNumber += _phonenumber.ElementAt(i);
                }
            }

            Regex _regex = new Regex(@"[^\d]");
            PhoneNumber = _regex.Replace(PhoneNumber, "");
            PhoneNumber = Regex.Replace(PhoneNumber, @"(\d{3})(\d{3})(\d{2})(\d{2})", "$1-$2 $3 $4");
            return PhoneNumber;
        }
    }
}
