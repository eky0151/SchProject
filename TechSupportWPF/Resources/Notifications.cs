using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DesktopToast;
using System.IO;
using NotificationsExtensions;
using NotificationsExtensions.Toasts;
using SchProject.ServiceBus;

namespace SchProject.Resources
{
    public class Notifications
    {
        public async void ShowStatusUpdateAsync(object sender, ServiceBus.StatusChangedEventArgs e)
        {
            var file = $"file:///{Path.GetFullPath("logo.png")}";
            await ShowSimpleNotification($"{e.Username} updated his/her status", "The new status is " + e.Status, file);
        }

        public async void ShowMessageAsync(object sender, ServiceBus.MessageEventArgs e)
        {
            await ShowMessageToastAsync(e.Sender, e.Message);
        }

        public async Task ShowSimpleNotification(string title, string body, string picturePath)
        {
            var request = new ToastRequest
            {
                ToastTitle = title,
                ToastBody = body,
                ToastLogoFilePath = picturePath,
                ShortcutFileName = "TechSupport.lnk",
                ShortcutTargetFilePath = Assembly.GetExecutingAssembly().Location,
                AppId = "TechSupport.Wpf",
                ActivatorId = typeof(NotificationActivator).GUID // For Action Center of Windows 10
            };

            var result = await ToastManager.ShowAsync(request);
        }
        public async Task ShowMessageToastAsync(string username, string message)
        {
            var request = new ToastRequest
            {
                ToastXml = ComposeInteractiveToast(username, message),
                ShortcutFileName = "TechSupport.lnk",
                ShortcutTargetFilePath = Assembly.GetExecutingAssembly().Location,
                AppId = "TechSupport.Wpf",
                ActivatorId = typeof(NotificationActivator).GUID
            };

            var result = await ToastManager.ShowAsync(request);
        }

        private string ComposeInteractiveToast(string username, string message)
        {
            var toastVisual = new ToastVisual
            {
                BindingGeneric = new ToastBindingGeneric
                {
                    Children =
                    {
                        new AdaptiveText { Text = $"{username} sent you a message" }, // Title
						new AdaptiveText { Text = message}, // Body
                    },
                    AppLogoOverride = new ToastGenericAppLogo
                    {
                        Source = $"file:///{Path.GetFullPath("logo.png")}",
                        AlternateText = "Logo"
                    }
                }
            };
            var toastAction = new ToastActionsCustom
            {
                Inputs =
                {
                    new ToastTextBox(id: username) { PlaceholderContent = "Input a message" }
                },
                Buttons =
                {
                    new ToastButton(content: "Reply", arguments: "action=Replied") { ActivationType = ToastActivationType.Background },
                    new ToastButton(content: "Ignore", arguments: "action=Ignored")
                }
            };
            var toastContent = new ToastContent
            {
                Visual = toastVisual,
                Actions = toastAction,
                Duration = ToastDuration.Long,
                Audio = new NotificationsExtensions.Toasts.ToastAudio
                {
                    Loop = true,
                    Src = new Uri("ms-winsoundevent:Notification.Looping.Alarm4")
                }
            };

            return toastContent.GetContent();
        }
    }
}
