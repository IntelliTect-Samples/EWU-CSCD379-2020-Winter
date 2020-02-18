using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Web.Api
{

    [ModelMetadataType(typeof(UserInputMetadata))]
    public partial class GroupInput
    {

    }
    public class GroupInputMetadata
    {
    }
}
