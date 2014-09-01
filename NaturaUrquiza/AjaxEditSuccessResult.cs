using System.Web.Mvc;

namespace NaturaUrquiza
{
    public class AjaxEditSuccessResult : ContentResult
    {
        public const string SuccessValue = "ajax-edit-success";

        public AjaxEditSuccessResult()
        {
            Content = SuccessValue;
        }
    }
}