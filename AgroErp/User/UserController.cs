using AgroErp.Data.DbConnection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;

namespace AgroErp.Controllers.UserInfo
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class UserController : ControllerBase
    {
        private readonly DefaultDbContext _context;
        private readonly UserInterface iUser;

        public UserController(DefaultDbContext context, UserInterface user)
        {
            _context = context;
            iUser = user;
        }


        // POST: api/User/LoginUser
        [HttpPost("LoginUser")]
        public async Task<ActionResult> LoginUser()
        {
            var response = await iUser.LoginUser(User.Identity.Name);
            return Ok(response);       
        }


        // POST: api/User/OTPSubmit
        [HttpPost("OTPSubmit")]
        public async Task<ActionResult> OTPSubmit(Api_AuthInfo authInfo)
        {
            var response = await iUser.OTPSubmit(User.Identity.Name, authInfo);
            return Ok(response);
        }


        // POST: api/User/OTPSubmit
        [AllowAnonymous]
        [HttpPost("OTPSubmitIfForgot")]
        public async Task<ActionResult> OTPSubmitIfForgot(Api_AuthInfo authInfo)
        {
            var response = await iUser.OTPSubmitIfForgot(authInfo.loginID, authInfo);
            return Ok(response);
        }


        // POST: api/User/ChangePassword
        [HttpPost("ChangePassword")]
        //[Obsolete]
        public async Task<ActionResult> ChangePassword(Password password)
        {
            var response = await iUser.ChangePassword(User.Identity.Name, password);
            return Ok(response);
        }


        // POST: api/User/ChangePasswordIfForgot
        [AllowAnonymous]
        [HttpPost("ChangePasswordIfForgot")]
        public async Task<ActionResult> ChangePasswordIfForgot(Password password)
        {
            var response = await iUser.ChangePasswordIfForgot(password);
            return Ok(response);
        }



        // POST: api/User/OTP_Generate
        // Authorization by Header Key and Value
        [AllowAnonymous]
        [HttpPost("OTP_Generate")]
        public async Task<ActionResult> OTP_Generate(User user)
        {
            string authKey = "BUROBD-GP_OTP", authValue ="";
            authValue = HttpContext.Request.Headers[authKey][0].ToString();

            var response = await iUser.OTP_Generate(user, authKey, authValue);
            return Ok(response);
        }

        private async Task<Result> OTP_send(User user)
        {
            Result result = new Result();

            try
            {

                #region OTP Add
                API_OTP new_OTP = new API_OTP();
                Random randomOTP = new Random();
                new_OTP.LoginID = user.LoginId;
                new_OTP.mobileOtp = randomOTP.Next(1111, 9999).ToString();
                new_OTP.emailOtp = randomOTP.Next(2222, 9999).ToString();
                new_OTP.validateUpToMobileOtp = DateTime.Now.AddMinutes(5);
                new_OTP.validateUpToEmailOtp = DateTime.Now.AddMinutes(5);

                var api_OTP = await _context.API_OTP.FindAsync(user.LoginId);
                if (api_OTP != null)
                {
                    _context.API_OTP.Remove(api_OTP);
                    _context.API_OTP.Add(new_OTP);
                }
                else _context.API_OTP.Add(new_OTP);

                await _context.SaveChangesAsync();

                #endregion


                var api_AuthInfo = await _context.Api_AuthInfo.FindAsync(user.LoginId);
                if (api_AuthInfo == null)
                {
                    result.success = false;
                    result.messageEn = "Invalid User information";
                    result.messageBn = "ইউজারের তথ্য সঠিক নয় ";
                    result.data = null;
                    return result;
                }

                var data = new
                {
                    OtpToMobile = api_AuthInfo.otpToMobile,
                    OtpToEmail = api_AuthInfo.otpToEmail,
                    mobile = api_AuthInfo.mobile,
                    email = api_AuthInfo.email,
                    //token = api_AuthInfo.token,
                    OtpValidity = "5 Minutes"
                };


                //OTP Send To Mail
                if (api_AuthInfo.otpToEmail)
                {
                    string SmtpHost = "", SmtpPort = "", SmtpNetUser = "", SmtpNetUserPassword = "", FromEmailAddress = "";
                    var mailSettings = await _context.MailSettings.ToListAsync();

                    foreach (var setting in mailSettings)
                    {
                        SmtpHost = setting.SmtpHost;
                        SmtpPort = setting.SmtpPort;
                        SmtpNetUser = setting.SmtpNetUser;
                        SmtpNetUserPassword = setting.SmtpNetUserPassword;
                        FromEmailAddress = setting.FromEmailAddress;
                        break;
                    }

                    MailSender ms = new MailSender();
                    ms.SendMailBySmtpClient(api_AuthInfo.email, "BUROBD - OTP", "আপনার ই-মেইল OTP হচ্ছে  <b><font color=\"Orange\" size=3px>" + new_OTP.emailOtp + "</font></b>", SmtpHost, SmtpPort, SmtpNetUser, SmtpNetUserPassword, FromEmailAddress);
                }


                //OTP Send To Mobile
                if (api_AuthInfo.otpToMobile)
                {
                    string urlRequest = "https://gpcmp.grameenphone.com/ecmapigw/webresources/ecmapigw.v2";
                    string SMS_User = "", SMS_Password = "";

                    #region Get UserID and Password
                    IConfigurationBuilder builder = new ConfigurationBuilder();
                    builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
                    var root = builder.Build();
                    var SmsSettings = root.GetSection("SmsSettings").GetChildren().ToList();

                    foreach (var path in SmsSettings)
                    {
                        if (path.Key == "SMS_GpUser") SMS_User = path.Value;
                        if (path.Key == "SMS_GpPassword") SMS_Password = path.Value;
                    }
                    #endregion

                    WebRequest request = WebRequest.Create(urlRequest);
                    request.Method = "POST";
                    request.ContentType = "application/json";
                    string requestBody = "{\"username\": \"" + SMS_User + "\",\"password\": \""+ SMS_Password + "\",\"apicode\": \"1\",\"msisdn\": \"" + api_AuthInfo.mobile + "\",\"countrycode\": \"880\",\"cli\": \"buro bd\",\"messagetype\": \"3\",\"message\": \"আপনার OTP হচ্ছে " + new_OTP.mobileOtp + "\",\"messageid\": \"0\"}";

                    byte[] byteArray = Encoding.UTF8.GetBytes(requestBody);
                    request.ContentLength = byteArray.Length;
                    Stream dataStream = request.GetRequestStream();
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();
                    WebResponse response = request.GetResponse();
                    dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string resp = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                    response.Close();


                    #region SMS Send threading
                    //string bodyContent2 = "{\"username\": \"" + SMS_User + "\",\"password\": \"" + SMS_Password + "\",\"apicode\": \"1\",\"msisdn\": \"" + api_AuthInfo.mobile + "\",\"countrycode\": \"880\",\"cli\": \"buro bd\",\"messagetype\": \"3\",\"message\": \"আপনার OTP হচ্ছে " + new_OTP.mobileOtp + "\",\"messageid\": \"0\"}";
                    //SmsGP smsGP = JsonConvert.DeserializeObject<SmsGP>(bodyContent2);
                    //var SmsResponse = await SMS_GP.SendSMS(smsGP);
                    #endregion

                    string status = resp.Substring(15, 3);
                    //string status = SmsResponse.Substring(15, 3);

                    if (status != "200")
                    {
                        result.success = false;
                        result.messageEn = resp;
                        result.messageBn = resp;
                        result.data = data;
                        return result;
                    }
                    else
                    {

                        result.success = true;
                        result.messageEn = "OTP generated successfully";
                        result.messageBn = "OTP সফলভাবে তৈরি হয়েছে";
                        result.data = data;
                        return result;
                    }

                }


                result.success = true;
                result.messageEn = "OTP generated successfully";
                result.messageBn = "OTP সফলভাবে তৈরি হয়েছে";
                result.data = data;
                return result;
            }catch
            {
                result.success = false;
                result.messageEn = "SMS Portal did not respond";
                result.messageBn = "এসএমএস পোর্টাল সাড়া দেয়নি";
                result.data = null;
                return result;
            }
        }


        //[HttpPost("ResetPassword")]
        //public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordRequest resetPassword)
        //{
        //    var response = await iUser.ResetPassword(User.Identity.Name, resetPassword);
        //    return Ok(response);
        //}


    }
}
