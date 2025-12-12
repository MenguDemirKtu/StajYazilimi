using Microsoft.AspNetCore.Mvc.Rendering;

namespace UniStaj
{
    public partial class ModelTabani
    {
        #region hazirlar


        public List<SelectListItem> evetHayirCek()
        {
            return _evetHayir();
        }
        #endregion
    }
}









