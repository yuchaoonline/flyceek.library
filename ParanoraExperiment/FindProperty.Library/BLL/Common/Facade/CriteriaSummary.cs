using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FP=FindProperty.Lib.BLL.Findproperty;

namespace FindProperty.Lib.BLL.Common.Facade
{
    public class CriteriaSummary
    {
        private bool ComparisonMinMax(decimal inValue, decimal cMin, decimal cMax)
        {
            bool result = true;
            if (cMin > 0 && cMax > 0)
            {
                result = (inValue >= cMin && inValue <= cMax);
            }
            else
            {
                if (cMin > 0)
                {
                    result = (inValue >= cMin);
                }
                if (cMax > 0)
                {
                    result = (inValue <= cMax);
                }
            }
            return result;
        }

        private int AddCount(object count)
        {
            if (count == null) { count = 1; }
            else { count = (int)count + 1; }
            return (int)count;
        }

        public ViewModel.CriteriaSummary Summary(List<FP.ViewModel.Post> posts, ViewModel.CriteriaSummary criterias)
        {
            if (posts != null && posts.Count() > 0)
            {
                posts.ForEach((x) =>
                {
                    if (x.PostType.ToUpper() == "S")
                    {
                        criterias.SalePrices.ForEach((a) =>
                        {
                            if ( ComparisonMinMax(x.Price.Value, a.Min, a.Max))
                            {
                                a.WParam = AddCount(a.WParam);
                            }
                        });
                    }
                    else
                    {
                        criterias.RentPrices.ForEach((a) =>
                        {
                            if ( ComparisonMinMax(x.Rental.Value, a.Min, a.Max))
                            {
                                a.LParam = AddCount(a.LParam);
                            }
                        });
                    }

                    criterias.Sizes.ForEach((b) =>
                    {
                        if ( ComparisonMinMax(x.Size, b.Min, b.Max))
                        {
                            if (x.PostType.ToUpper() == "S")
                            {
                                b.WParam = AddCount(b.WParam);
                            }
                            else
                            {
                                b.LParam = AddCount(b.LParam);
                            }
                        }
                    });

                    criterias.BedRooms.ForEach((c) =>
                    {
                        if ( x.BedroomCount == c.Min && x.BedroomCount == c.Max)
                        {
                            if (x.PostType.ToUpper() == "S")
                            {
                                if (c.WParam == null) { c.WParam = 1; }
                                else { c.WParam = (int)c.WParam + 1; }
                            }
                            else
                            {
                                if (c.LParam == null) { c.LParam = 1; }
                                else { c.LParam = (int)c.LParam + 1; }
                            }
                        }
                    });

                });
            }

            return criterias;
        }
    }
}
