//using Base.Shared.ResultUtility;
//using System.Text.Json.Serialization;

//namespace Base.Shared
//{

//    public class ResultHandler
//    {
//        [JsonPropertyName("isSuccess")]
//        public bool IsSuccess { get; set; }
//        [JsonPropertyName("message")]
//        public string[] Message { get; set; }

//        public static ResultHandler MapToResultHandler(ResultOperation resultOperation)
//        {
//            return new ResultHandler
//            {
//                IsSuccess = resultOperation.isSuccess,
//                Message = resultOperation?.message?.ToArray(),
//            };
//        }

//    }
//    public class ResultHandler<T>
//    {
//        [JsonPropertyName("isSuccess")]
//        public bool IsSuccess { get; set; }
//        [JsonPropertyName("message")]
//        public string[] Message { get; set; }
//        [JsonPropertyName("data")]
//        public T Data { get; set; }
//        public static ResultHandler<T> MapToResultHandler(ResultOperation<T> resultOperation)
//        {
//            return new ResultHandler<T>
//            {
//                IsSuccess = resultOperation.isSuccess,
//                Message = resultOperation?.message?.ToArray(),
//                Data = resultOperation.Data

//            };
//        }

//    }

//}
