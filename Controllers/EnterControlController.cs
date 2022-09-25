using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp4.DB;

namespace WpfApp4.Controllers
{
    public class EnterControlController
    {
        private DB.MyContext MyContext;
        private string errorMessage = "Error \n";
        public EnterControlController(MyContext myContext, string errorMessage)
        {
            MyContext = new DB.MyContext();
        }
        internal List<ModelView.EnterControlView> GetEnterControlTheLastFiveDays()
        {
            var lResult = new List<ModelView.EnterControlView>();
            try
            {
                List<DB.Acaunt> acaunts = MyContext.Acaunts.ToList();
                foreach (var acauntDb in acaunts)
                {
                    var newModelAcaunting = new ModelView.EnterControlView();

                    newModelAcaunting.NameEdnMessage = $"User {acauntDb.Name} => " + $"user in {GetLastEnter(acauntDb.AcauntId)}";
                    newModelAcaunting.MyPathImage = @"pack://aplication:,,,/AcauntImage/" + acauntDb.PathImage;

                    newModelAcaunting.ColorBorder = GetColorBorder(DateTime.Now, acauntDb.AcauntId);
                    newModelAcaunting.ColorBorder2 = GetColorBorder(DateTime.Now.AddDays(-1), acauntDb.AcauntId);
                    newModelAcaunting.ColorBorder3 = GetColorBorder(DateTime.Now.AddDays(-2), acauntDb.AcauntId);
                    newModelAcaunting.ColorBorder4 = GetColorBorder(DateTime.Now.AddDays(-3), acauntDb.AcauntId);
                    newModelAcaunting.ColorBorder5 = GetColorBorder(DateTime.Now.AddDays(-4), acauntDb.AcauntId);
                    newModelAcaunting.IdAccaunt = acauntDb.AcauntId;
                    lResult.Add(newModelAcaunting);
                }
                return lResult;
            }
            catch (Exception ex)
            {
                throw new Exception(errorMessage + ex.Message);
            }
        }

        private string GetColorBorder(DateTime now, int acauntId)
        {
            var dateMin = DateTime.Parse($"{now.Year}.{now.Month}.{now.Day}");
            var dateMax = dateMin.AddDays(1);

            try
            {
                var flag = MyContext.EnterControls.Any(x => x.AcauntId == acauntId && x.DateTimeEnterControl >= dateMin && x.DateTimeEnterControl < dateMax);
                if (flag)
                    return "Green";
                else return "Red";
            }
            catch (Exception ex)
            {
                throw new Exception(errorMessage+ex.Message);
            }
        }

        private string GetLastEnter(int acauntId)
        {
            try
            {
                var data = MyContext.EnterControls.Where(x => x.AcauntId == acauntId).Max(x => x.DateTimeEnterControl);
                return data.ToLongDataString();
            }
            catch (ArgumentNullException ex)
            {
                return "No";
            }
            catch (InvalidOperationException ex)
            {
                return "No";
            }
            catch (Exception ex)
            {
                throw new Exception("Error Base Migration");
            }
        }
    }
}
