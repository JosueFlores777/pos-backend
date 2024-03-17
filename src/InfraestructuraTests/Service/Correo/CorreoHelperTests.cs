﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Infraestructura.Service.Correo;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Infraestructura.Service.Correo.Tests
{
    [TestClass()]
    public class CorreoHelperTests
    {
        [TestMethod()]
        public void EnviarCorreoPermisoCompletoTest()
        {
            var text = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test.html"));

            var encabezado = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n  <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" />\r\n  <meta name=\"viewport\" content=\"width=device-width, initial-scale=1\" />\r\n  <title>Oxygen Confirm</title>\r\n\r\n  <style type=\"text/css\">\r\n    /* Take care of image borders and formatting, client hacks */\r\n    img { max-width: 600px; outline: none; text-decoration: none; -ms-interpolation-mode: bicubic;}\r\n    a img { border: none; }\r\n    table { border-collapse: collapse !important;}\r\n    #outlook a { padding:0; }\r\n    .ReadMsgBody { width: 100%; }\r\n    .ExternalClass { width: 100%; }\r\n    .backgroundTable { margin: 0 auto; padding: 0; width: 100% !important; }\r\n    table td { border-collapse: collapse; }\r\n    .ExternalClass * { line-height: 115%; }\r\n    .container-for-gmail-android { min-width: 600px; }\r\n\r\n\r\n    /* General styling */\r\n    * {\r\n      font-family: Helvetica, Arial, sans-serif;\r\n    }\r\n\r\n    body {\r\n      -webkit-font-smoothing: antialiased;\r\n      -webkit-text-size-adjust: none;\r\n      width: 100% !important;\r\n      margin: 0 !important;\r\n      height: 100%;\r\n      color: #676767;\r\n    }\r\n\r\n    td {\r\n      font-family: Helvetica, Arial, sans-serif;\r\n      font-size: 14px;\r\n      color: #777777;\r\n      text-align: center;\r\n      line-height: 21px;\r\n    }\r\n\r\n    a {\r\n      color: #676767;\r\n      text-decoration: none !important;\r\n    }\r\n\r\n    .pull-left {\r\n      text-align: left;\r\n    }\r\n\r\n    .pull-right {\r\n      text-align: right;\r\n    }\r\n\r\n    .header-lg,\r\n    .header-md,\r\n    .header-sm {\r\n      font-size: 32px;\r\n      font-weight: 700;\r\n      line-height: normal;\r\n      padding: 35px 0 0;\r\n      color: #4d4d4d;\r\n    }\r\n\r\n    .header-md {\r\n      font-size: 24px;\r\n    }\r\n\r\n    .header-sm {\r\n      padding: 5px 0;\r\n      font-size: 18px;\r\n      line-height: 1.3;\r\n    }\r\n\r\n    .content-padding {\r\n      padding: 20px 0 5px;\r\n    }\r\n\r\n    .mobile-header-padding-right {\r\n      width: 290px;\r\n      text-align: right;\r\n      padding-left: 10px;\r\n    }\r\n\r\n    .mobile-header-padding-left {\r\n      width: 290px;\r\n      text-align: left;\r\n      padding-left: 10px;\r\n    }\r\n\r\n    .free-text {\r\n      width: 100% !important;\r\n      padding: 10px 60px 0px;\r\n    }\r\n\r\n    .button {\r\n      padding: 30px 0;\r\n    }\r\n\r\n\r\n    .mini-block {\r\n      border: 1px solid #e5e5e5;\r\n      border-radius: 5px;\r\n      background-color: #ffffff;\r\n      padding: 12px 15px 15px;\r\n      text-align: left;\r\n      width: 253px;\r\n    }\r\n\r\n    .mini-container-left {\r\n      width: 278px;\r\n      padding: 10px 0 10px 15px;\r\n    }\r\n\r\n    .mini-container-right {\r\n      width: 278px;\r\n      padding: 10px 14px 10px 15px;\r\n    }\r\n\r\n    .product {\r\n      text-align: left;\r\n      vertical-align: top;\r\n      width: 175px;\r\n    }\r\n\r\n    .total-space {\r\n      padding-bottom: 8px;\r\n      display: inline-block;\r\n    }\r\n\r\n    .item-table {\r\n      padding: 50px 20px;\r\n      width: 560px;\r\n    }\r\n\r\n    .item {\r\n      width: 300px;\r\n    }\r\n\r\n    .mobile-hide-img {\r\n      text-align: left;\r\n      width: 125px;\r\n    }\r\n\r\n    .mobile-hide-img img {\r\n      border: 1px solid #e6e6e6;\r\n      border-radius: 4px;\r\n    }\r\n\r\n    .title-dark {\r\n      text-align: left;\r\n      border-bottom: 1px solid #cccccc;\r\n      color: #4d4d4d;\r\n      font-weight: 700;\r\n      padding-bottom: 5px;\r\n    }\r\n\r\n    .item-col {\r\n      padding-top: 20px;\r\n      text-align: left;\r\n      vertical-align: top;\r\n    }\r\n\r\n    .force-width-gmail {\r\n      min-width:600px;\r\n      height: 0px !important;\r\n      line-height: 1px !important;\r\n      font-size: 1px !important;\r\n    }\r\n\r\n  </style>\r\n\r\n  <style type=\"text/css\" media=\"screen\">\r\n    @import url(http://fonts.googleapis.com/css?family=Oxygen:400,700);\r\n  </style>\r\n\r\n  <style type=\"text/css\" media=\"screen\">\r\n    @media screen {\r\n      /* Thanks Outlook 2013! */\r\n      * {\r\n        font-family: 'Oxygen', 'Helvetica Neue', 'Arial', 'sans-serif' !important;\r\n      }\r\n    }\r\n  </style>\r\n\r\n  <style type=\"text/css\" media=\"only screen and (max-width: 480px)\">\r\n    /* Mobile styles */\r\n    @media only screen and (max-width: 480px) {\r\n\r\n      table[class*=\"container-for-gmail-android\"] {\r\n        min-width: 290px !important;\r\n        width: 100% !important;\r\n      }\r\n\r\n      img[class=\"force-width-gmail\"] {\r\n        display: none !important;\r\n        width: 0 !important;\r\n        height: 0 !important;\r\n      }\r\n\r\n      table[class=\"w320\"] {\r\n        width: 320px !important;\r\n      }\r\n\r\n\r\n      td[class*=\"mobile-header-padding-left\"] {\r\n        width: 160px !important;\r\n        padding-left: 0 !important;\r\n      }\r\n\r\n      td[class*=\"mobile-header-padding-right\"] {\r\n        width: 160px !important;\r\n        padding-right: 0 !important;\r\n      }\r\n\r\n      td[class=\"header-lg\"] {\r\n        font-size: 24px !important;\r\n        padding-bottom: 5px !important;\r\n      }\r\n\r\n      td[class=\"content-padding\"] {\r\n        padding: 5px 0 5px !important;\r\n      }\r\n\r\n       td[class=\"button\"] {\r\n        padding: 5px 5px 30px !important;\r\n      }\r\n\r\n      td[class*=\"free-text\"] {\r\n        padding: 10px 18px 30px !important;\r\n      }\r\n\r\n      td[class~=\"mobile-hide-img\"] {\r\n        display: none !important;\r\n        height: 0 !important;\r\n        width: 0 !important;\r\n        line-height: 0 !important;\r\n      }\r\n\r\n      td[class~=\"item\"] {\r\n        width: 140px !important;\r\n        vertical-align: top !important;\r\n      }\r\n\r\n      td[class~=\"quantity\"] {\r\n        width: 50px !important;\r\n      }\r\n\r\n      td[class~=\"price\"] {\r\n        width: 90px !important;\r\n      }\r\n\r\n      td[class=\"item-table\"] {\r\n        padding: 30px 20px !important;\r\n      }\r\n\r\n      td[class=\"mini-container-left\"],\r\n      td[class=\"mini-container-right\"] {\r\n        padding: 0 15px 15px !important;\r\n        display: block !important;\r\n        width: 290px !important;\r\n      }\r\n    }\r\n  </style>\r\n</head>\r\n\r\n<body bgcolor=\"#f7f7f7\">\r\n<table align=\"center\" cellpadding=\"0\" cellspacing=\"0\" class=\"container-for-gmail-android\" width=\"100%\">\r\n  <tr>\r\n    <td align=\"left\" valign=\"top\" width=\"100%\" style=\"background:repeat-x url(http://s3.amazonaws.com/swu-filepicker/4E687TRe69Ld95IDWyEg_bg_top_02.jpg) #ffffff;\">\r\n      <center>\r\n      <img src=\"http://s3.amazonaws.com/swu-filepicker/SBb2fQPrQ5ezxmqUTgCr_transparent.png\" class=\"force-width-gmail\">\r\n        <table cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" bgcolor=\"#ffffff\" background=\"http://s3.amazonaws.com/swu-filepicker/4E687TRe69Ld95IDWyEg_bg_top_02.jpg\" style=\"background-color:transparent\">\r\n          <tr>\r\n            <td width=\"100%\" height=\"80\" valign=\"top\" style=\"text-align: center; vertical-align:middle;\">\r\n         \r\n            </td>\r\n          </tr>\r\n        </table>\r\n      </center>\r\n    </td>\r\n  </tr>\r\n  <tr>\r\n    <td align=\"center\" valign=\"top\" width=\"100%\" style=\"background-color: #f7f7f7;\" class=\"content-padding\">\r\n      <center>\r\n        <table cellspacing=\"0\" cellpadding=\"0\" width=\"600\" class=\"w320\">\r\n          <tr>\r\n            <td class=\"header-lg\">\r\n              SENASA TE NOTIFICA\r\n            </td>\r\n          </tr>\r\n          <tr>\r\n\t\t  \r\n            <td class=\"free-text\">\r\n             <h3>La solicitud numero <span style=\"color: #28A745;\">IDSOLICITUD</span>. ha sido gestionada por SEPA<h3>\r\n\t\t\t\r\n\t\t\t <h3>Información <span style=\"color: #28A745;\">CORREOSEPA</span><h3>\r\n            </td>\r\n          </tr>\r\n          <tr>\r\n            <td class=\"button\">\r\n              <div><a href=\"http://\"\r\n              style=\"background-color:#ff6f6f;border-radius:5px;color:#ffffff;display:inline-block;font-family:'Cabin', Helvetica, Arial, sans-serif;font-size:14px;font-weight:regular;line-height:45px;text-align:center;text-decoration:none;width:155px;-webkit-text-size-adjust:none;mso-hide:all;\">Ir al portal</a></div>\r\n            </td>\r\n          </tr>\r\n          <tr>\r\n            <td class=\"w320\">\r\n              <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\r\n                <tr>\r\n\r\n                  <td class=\"mini-container-right\">\r\n                    <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\r\n                      <tr>\r\n                        ";

            var detalle = "";
            for (int i = 0; i < 3; i++)
            {
                var item = "<td class=\"mini-block-padding\">\r\n                          <table cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"border-collapse:separate !important;\">\r\n                            <tr>\r\n                              <td class=\"mini-block\">\r\n                                <span class=\"header-sm\">NOMBRECOMUN</span><br />\r\n                                NOMBRECIENTIFICO <br />\r\n                                <br />\r\n                                <span class=\"header-sm\">Estado</span> <br />\r\n                                ESTADOSEPA\r\n                              </td>\r\n                            </tr>\r\n                          </table>\r\n                        </td>\r\n";

                item = item.Replace("NOMBRECOMUN", "CEBOLLA");
                item = item.Replace("ESTADOSEPA", "NOMBREcIENTIFICO");
                detalle = detalle + item;
            }
            var pie = "                      </tr>\r\n                    </table>\r\n                  </td>\r\n                </tr>\r\n              </table>\r\n            </td>\r\n          </tr>\r\n        </table>\r\n      </center>\r\n    </td>\r\n  </tr>\r\n <tr>\r\n    <td align=\"center\" valign=\"top\" width=\"100%\" style=\"background-color: #f7f7f7; height: 100px;\">\r\n      <center>\r\n        <table cellspacing=\"0\" cellpadding=\"0\" width=\"600\" class=\"w320\">\r\n          <tr>\r\n            <td style=\"padding: 25px 0 25px\">\r\n              <strong>SENASA</strong><br />\r\n              © Copyright © 2020 SENASA. <br />\r\n              Servicio Nacional de Sanidad e Inocuidad Agroalimentaria <br /><br />\r\n            </td>\r\n          </tr>\r\n        </table>\r\n      </center>\r\n    </td>\r\n  </tr>\r\n</table>\r\n</div>\r\n</body>\r\n</html>";

            var resultado = encabezado + detalle + pie;

            Assert.IsTrue(resultado.Contains("CEBOLLA"));
        }
    }
}