using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Findproperty.DALFactory
{
    public interface IDataAccessFactory
    {      
        IDAL.IGscp Gscp();


        IDAL.IRegion Region();

        IDAL.IScp Scp();

        IDAL.IPost Post();

        IDAL.IPostImage PostImage();

        IDAL.IPostItem PostItem();

        IDAL.ICenest Cenest();
        
        IDAL.IAgent Agent();

        IDAL.IEnquiry Enquiry();

        IDAL.IEstScore EstScore();

        IDAL.IBranch Branch();

        IDAL.IAppUpdate AppUpdate();
    }
}
