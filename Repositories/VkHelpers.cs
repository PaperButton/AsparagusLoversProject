
using Newtonsoft.Json;
using System.Net;
using System.Runtime.Serialization;
using System.Text;



namespace AsparagusLoversProject.Repositories
{
    public static class VkHelpers
    {
        public static VKUserNameData GetVKUserNameData(String userNameId, String access_token)
        {
            /*var accessTokenUrl = GetAccessTokenUrl(app_id, app_secret);
            var responseStrAccessToken = GetRequest(accessTokenUrl);*/
            var url = GetVKUserNameDataUrl(userNameId, access_token);
            var responseStr = GetRequest(url);
            return JsonConvert.DeserializeObject<VKUserNameData>(responseStr);
        }

        public static string GetRequest(string url)
        {
            WebRequest wr = WebRequest.Create(url);

            using (Stream objStream = wr.GetResponse().GetResponseStream())
            {
                using (StreamReader objReader = new StreamReader(objStream))
                {
                    StringBuilder sb = new StringBuilder();
                    string line = "";
                    while (true)
                    {
                        line = objReader.ReadLine();

                        if (line != null)
                        {
                            sb.Append(line);
                        }
                        else
                        {
                            return sb.ToString();
                        }
                    }
                }
            }
        }
        public static string GetAccessTokenUrl(String app_id, String app_secret)
        {
            /* return String.Format(@"https://oauth.vk.com/access_token?client_id={0}&client_secret={1}", app_id, app_secret);*/
            return String.Format(@"https://oauth.vk.com/authorize?сlient_id={0}&display=page&redirect_uri=https://www.google.com&response_type=token&v=5.21", app_id);
        }
        public static string GetVKUserNameDataUrl(String userNameId, String accessToken)
        {
            return String.Format(@"https://api.vk.com/method/users.get?user_ids={0}&access_token={1}&v=5.131", userNameId, accessToken);
        }
    }



    


    [Serializable]
    public class VKUserNameData
    {
        private VKUserNameDataItem[] _vkUserNameDataItems;


        [JsonProperty("response")]
        public VKUserNameDataItem[] VKUserNameDataItems { get { return _vkUserNameDataItems; } set { _vkUserNameDataItems = value; } }

    }

    [Serializable]
    public class VKUserNameDataItem
    {
        private string _userName;
        private string _userSurname;
        private int _userId;

        [JsonProperty("first_name")]
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        [JsonProperty("last_name")]
        public string UserSurname
        {
            get { return _userSurname; }
            set { _userSurname = value; }
        }

        [JsonProperty("id")]
        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
    }


}
