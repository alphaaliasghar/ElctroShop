using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectonShop.Application.Convertor
{
    public static class DataConvertor
    {
        public static string ToShamsi(this DateTime value)
        {
            PersianCalendar pc=new PersianCalendar();
            return pc.GetYear(value)+"/"+pc.GetMonth(value)
                .ToString("00")+"/"+pc.GetDayOfMonth(value).ToString("00");
        }
    }
}
