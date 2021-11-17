using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Hieu.FinalProject.Branchs
{
    public interface IBranchService: ICrudAppService<BranchDto, Guid,
        BranchPageDto, CreateUpdateBranchDto>
    {

    }
}
