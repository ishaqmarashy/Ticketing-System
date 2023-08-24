using System;
using Newtonsoft.Json.Linq;

namespace Ticketing_System.DataAccess
{
    //access= 0 update 1 delete 2 update&delete chooses buttons
    //postfix specifies the unique value in the table so that it may set the id of the button to match it
    //this makes the actions of the buttons easy to manage through JS
    public class TableBuilder
    {
        public static string Build(JArray Jar, int? postfix, int? access)
        {
            if (postfix == null)
                access = null;
            else if (access == null)
                postfix = null;
            else if (access < 0 && access >= 3)
            {
                access = null;
                postfix = null;
            }
            try
            {
                var result = " <table id=\"myTable\" class=\"table table-striped\" style=\"height: 100 %; width: 100 %;\" >   <thead>";
                int i = 0;
                result += " <tr> ";
                result += " <th scope=\"col\"> ROW </th>";
                Boolean postFixed = postfix != null;
                var identifier = "";
                var identifierMod = "";
                var identifierLoc = postfix;
                if (Jar!=null )
                foreach (var col in Jar[0])
                {
                    identifier = col.ToString().Replace("\"", "").Split(":")[0];
                    if (i == identifierLoc)
                        identifierMod = identifier;
                    i++;
                    result += " <th scope=\"col\"> " + identifier + "</th>";
                }
                if (postFixed)
                {
                    if (postFixed)
                    {
                        result += " <th scope=\"row\">";
                        if (access == 0)
                            result += "UPDATE";
                        else if (access == 1)
                            result += "DELETE";
                        else result += "UPDATE</th><th>DELETE";
                        result += "</th>";
                    }
                }
                i = 0;
                identifierMod = "";
                result += " </tr>  </thead>  <tbody>";
                foreach (var row in Jar)
                {

                    result += " <tr id=\"" + i + "\">  ";
                    result += " <th id=\"" + i + "\" scope=\"row\">" + i + "</th>";
                    int ii = 0;
                    foreach (var col in row)
                    {
                        if (ii == identifierLoc)
                            identifierMod = col.Last.ToString();
                        result += " <th  scope=\"row\">" + col.Last + "</th>";
                        ii++;
                    }
                    if (postFixed)
                    {
                        result += " <th scope=\"row\">";
                        string btnUpdate = "<button class=\"btn btn-primary ripple-surface\" id=\"" +
                                           identifierMod + "\" name=\"" + i + "\" value=\"UPDATE\">UPDATE</button>";
                        string btnDelete = "<button class=\"btn btn-primary ripple-surface\" id=\"" +
                                           identifierMod + "\" name=\"" + i + "\" value=\"DELETE\">DELETE</button>";
                        if (access == 0)
                            result += btnUpdate;
                        else if (access == 1)
                            result += btnDelete;
                        else result += btnUpdate + "</th><th>" + btnDelete;
                        result += "</th>";
                    }
                    result += " </tr>";
                    i++;
                }
                return result += "  </tbody> </table>";
            }
            catch (Exception)
            {
                return "Invalid Input";
            }
        }
    }
}