using CloudX.Azure.Core.Utils;

namespace Cloud.Azure.AwesomePizzaSite.Api.Dto
{
    public class AbstractDto
    {
        public override string ToString()
        {
            return CommonUtils.WrapToJson(this);
        }
    }
}
