using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace TravelNinjaz.B2B.WebAPI.Models.Entity
{
    public static class Common
    {
        public enum Operation
        {
            ADD = 0,
            UPDATE = 1,
            DELETE = 2,
            ACTIVE = 3,
            INACTIVE = 4
        }

        public enum MessageType
        {
            InsertSuccess = 1,
            InsertFailed = 2,
            UpdateSuccess = 3,
            UpdateFailed = 4,
            DeleteSuccess = 5,
            DeleteFailed = 6,
            SuccessOnly = 7,
            FileSuccess = 8,
            FileFailed = 9,
            EmailExists = 10,
            FileRemoveSuccess = 11,
            FileRemoveFailed = 12,
            ReinviteSuccess = 13,
            ReinviteFailed = 14,
            ImportSuccessfully = 15,
            ExportSuccessfully = 16,
            SaveSuccess = 17,
            SaveFailed = 18,
        }

        public enum FileUploadAS
        {
            Logo = 1,
            Profile = 2,
            File = 3
        }

        public static string GetMessage(MessageType type)
        {
            string result = string.Empty;

            switch (type)
            {
                case MessageType.InsertSuccess:
                    result = " added successfully.";
                    break;
                case MessageType.InsertFailed:
                    result = "Failed to add ";
                    break;
                case MessageType.UpdateSuccess:
                    result = " updated successfully.";
                    break;
                case MessageType.UpdateFailed:
                    result = "Failed to update ";
                    break;
                case MessageType.DeleteSuccess:
                    result = " deleted successfully.";
                    break;
                case MessageType.DeleteFailed:
                    result = "Failed to delete ";
                    break;
                case MessageType.SuccessOnly:
                    result = "Success";
                    break;
                case MessageType.FileSuccess:
                    result = "File saved successfully.";
                    break;
                case MessageType.FileFailed:
                    result = "Failed to save file.";
                    break;
                case MessageType.EmailExists:
                    result = "Email already exists, try another.";
                    break;
                case MessageType.FileRemoveSuccess:
                    result = "File removed successfully.";
                    break;
                case MessageType.FileRemoveFailed:
                    result = "Failed to remove file.";
                    break;
                case MessageType.ReinviteSuccess:
                    result = "Invitation sent successfully.";
                    break;
                case MessageType.ReinviteFailed:
                    result = "Invitation failed to send.";
                    break;
                case MessageType.ImportSuccessfully:
                    result = "Data Imported successfully.";
                    break;
                case MessageType.ExportSuccessfully:
                    result = "Data Exported successfully.";
                    break;
                case MessageType.SaveSuccess:
                    result = " saved successfully.";
                    break;
                case MessageType.SaveFailed:
                    result = "Failed to Save ";
                    break;
            }
            return result;
        }

        public enum UserRole
        {
            ADMIN = 1,
            B2BCLIENT = 2,
            GUEST = 3
        }

        public enum FromApplicaiton
        {
            ADMIN = 1,
            B2B = 2          
        }

        public enum SocialMediaType
        {
            APPLICATION = 0,
            GOOGLE = 1,
            FACEBOOK = 2
        }

    }
}
