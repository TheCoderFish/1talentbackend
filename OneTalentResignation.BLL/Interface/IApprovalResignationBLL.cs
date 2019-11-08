using OneTalentResignation.DTO.DomainModel;
using OneTalentResignation.DTO.View_Models;
using System.Collections.Generic;

namespace OneTalentResignation.BLL.Interface
{
    /// <summary>
    /// Interface       :IApprovalResignationBLL
    /// Author          : Binal Patel
    /// Creation Date   : 17 oct 2019
    /// Purpose         : To implement Dependency Injection
    /// </summary>
    public interface IApprovalResignationBLL
    {

        List<string> GetDomain();

        List<string> GetStatus();

        List<string> GetDesignation();

        List<string> GetTechnology();

        List<ResignationListViewModel> GetResignationEmployeeList(ResignationFilterViewModel filter);

        EmployeeDetailViewModel GetResignationDetails(int resignationId);

        void Approve(ResignationViewModel resignationModel);       

        Resignation Put(ResignationViewModel resignationModel);

        
    }
}
