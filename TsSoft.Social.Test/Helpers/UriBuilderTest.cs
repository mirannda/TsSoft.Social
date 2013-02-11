namespace TsSoft.Social.Test.Helpers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TsSoft.Social.Vk;
    using TsHelpers = TsSoft.Social.Helpers;

    [TestClass]
    public class UriBuilderTest
    {
        [TestMethod]
        public void TestBuilder()
        {
            var builder = new TsHelpers.UriBuilder();
            builder.BaseUrl = "https://api.vkontakte.ru/method/notes.add";

            Assert.IsNotNull(builder);
            Assert.IsNotNull(builder.BaseUrl);
            Assert.AreEqual("https://api.vkontakte.ru/method/notes.add", builder.BaseUrl);

            builder.Append("title", "Заголовок");
            builder.Append("text", "Текст заметки");

            Assert.AreEqual(
                @"https://api.vkontakte.ru/method/notes.add?title=%d0%97%d0%b0%d0%b3%d0%be%d0%bb%d0%be%d0%b2%d0%be%d0%ba&text=%d0%a2%d0%b5%d0%ba%d1%81%d1%82+%d0%b7%d0%b0%d0%bc%d0%b5%d1%82%d0%ba%d0%b8",
                builder.Uri);
        }
    }
}