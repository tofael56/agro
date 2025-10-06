using Microsoft.AspNetCore.Http;

namespace AbadiAgroApi.Model.General
{
    public class Result
    {

        public bool success { get; set; }
        public string messageEn { get; set; }
        public string messageBn { get; set; }
        public int statusCode { get; set; }
        public object data { get; set; }



        // Static method for NotFound
        public static Result NotFound()
        {
            return new Result
            {
                success = false,
                messageEn = Messages.dataNotFoundEn,
                messageBn = Messages.dataNotFoundBn,
                statusCode = StatusCodes.Status404NotFound,
                data = null
            };
        }

        // Static method for DataAlreadyExist
        public static Result DataAlreadyExist()
        {
            return new Result
            {
                messageEn = Messages.alreadyExistsEn,
                messageBn = Messages.alreadyExistsBn,
                success = false,
                statusCode = StatusCodes.Status400BadRequest,
                data = null

            };
        }

        // Example for success method
        public static Result Success(string? messageEn = null, string? messageBn = null, object data = null)
        {
            return new Result
            {
                success = true,
                messageEn = string.IsNullOrEmpty(messageEn) ? Messages.successEn : messageEn,
                messageBn = string.IsNullOrEmpty(messageBn) ? Messages.successBn : messageBn,
                statusCode = StatusCodes.Status200OK,
                data = data
            };
        }

        public static Result Error()
        {
            return new Result
            {
                messageEn = Messages.failedEn,
                messageBn = Messages.failedBn,
                success = false,
                statusCode = StatusCodes.Status500InternalServerError,
                data = null
            };
        }

        public static Result BadRequest()
        {
            return new Result
            {
                messageEn = Messages.requiredDataEn,
                messageBn = Messages.requiredDataBn,
                success = false,
                statusCode = StatusCodes.Status400BadRequest,
                data = null
            };
        }
    }
}
