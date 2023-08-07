using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScortelApi.Models.PDF.Utility
{
    public static class TemplateGen
    {
        public static string GetHtml()
        {
            var list = DataStorage.GetAllEmployees();

            var sb = new StringBuilder();
            sb.Append(@"
                <html>
                    <head></head>
                    <body>
                        <div class='header'><h1> Tova e zaglavie </h1></div>
                        <table>
                            <tr>
                                <th>Name</th>
                                <th>Name</th>
                            </tr>");
            foreach (var emp in list)
            {
                sb.AppendFormat(@"
                    <tr>
                        <td>{0}</td>
                        <td>{1}</td>
                    </tr>", emp.Name, emp.Age);
                
            }
            sb.Append(@" </table> </body> </html>");

            return sb.ToString();
        }
    }
}
