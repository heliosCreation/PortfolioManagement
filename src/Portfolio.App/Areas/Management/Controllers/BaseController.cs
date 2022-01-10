using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Portfolio.Areas.Management.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Management")]
    public class BaseController : Controller
    {
        private readonly IMapper _mapper;
        protected IMapper Mapper => _mapper ?? HttpContext.RequestServices.GetService<IMapper>();
    }
}
