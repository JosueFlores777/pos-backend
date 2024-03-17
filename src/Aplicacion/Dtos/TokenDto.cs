using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Dtos
{
    public class TokenDto
    {
        public TokenDto(string token,string type, int time) {
            access_token = token;
            token_type = type;
            expires_in = time;
        }
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }

    }
}
