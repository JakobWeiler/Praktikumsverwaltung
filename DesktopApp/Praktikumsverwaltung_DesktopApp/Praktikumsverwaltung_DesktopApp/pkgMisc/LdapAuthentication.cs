using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.Protocols;
using System.Net;
using System.Security;

namespace Praktikumsverwaltung_DesktopApp.pkgMisc
{
    class LdapAuthentication
    {
        private String _path;
        private String _filterAttribute;

        public LdapAuthentication(String path)
        {
            _path = path;
        }
        

        public bool IsAuthenticatednew(String domain, String user, String pwd)
        {
            bool isValid = false;
            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, _path, domain + ":636", ContextOptions.Negotiate, user, pwd))
            {
                // validate the credentials
                isValid = pc.ValidateCredentials(user, pwd);

            }
            return isValid;
        }
    }
}
