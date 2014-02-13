using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace DailyNotes
{
    /// <summary>
    /// Cette classe simplifie l'affichage des toasts.
    /// </summary>
    class ToastHelper
    {
        public static void ShowToast(string content)
        {
            ToastTemplateType toastTemplate = ToastTemplateType.ToastText01; 
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
            XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
            toastTextElements[0].AppendChild(toastXml.CreateTextNode(content));
            ToastNotification toast = new ToastNotification(toastXml);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }

        public static void ShowToast(string header, string content)
        {
            ToastTemplateType toastTemplate = ToastTemplateType.ToastText02;
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
            XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
            toastTextElements[0].AppendChild(toastXml.CreateTextNode(header));
            toastTextElements[1].AppendChild(toastXml.CreateTextNode(content));
            ToastNotification toast = new ToastNotification(toastXml);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }

        public static void ShowToast(string header, string content, string imageUri)
        {
            ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
            XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
            toastTextElements[0].AppendChild(toastXml.CreateTextNode(header));
            toastTextElements[1].AppendChild(toastXml.CreateTextNode(content));

            var image = toastXml.GetElementsByTagName("image")[0];
            image.Attributes[1].AppendChild(toastXml.CreateTextNode(imageUri));
 
            ToastNotification toast = new ToastNotification(toastXml);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}
