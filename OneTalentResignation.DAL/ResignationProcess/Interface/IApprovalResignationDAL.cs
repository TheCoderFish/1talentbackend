using OneTalentResignation.DTO.DomainModel;
using OneTalentResignation.DTO.View_Models;
using System.Collections.Generic;

namespace OneTalentResignation.DAL.ResignationProcess.Interface
{
    /// <summary>
    /// Interface       :IApprovalResignationDAL
    /// Author          : Divya Patel
    /// Creation Date   : 17 oct 2019
    /// Purpose         : To implement Dependency Injection
    /// </summary>
    public interface IApprovalResignationDAL
    {
        List<string> GetDomain();

        List<string> GetStatus();

        List<string> GetDesignation();

        List<string> GetTechnology();

        List<ResignationListViewModel> GetResignationEmployeeList(ResignationFilterViewModel filter);

        EmployeeDetailViewModel GetResignationDetails(int resignationId,string subId);

        void Approve(ResignationViewModel resignationModel);      

        Resignation Put(ResignationViewModel resignationModel);

    }
}
