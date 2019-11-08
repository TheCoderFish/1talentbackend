using OneTalentResignation.DTO.DomainModel;
using OneTalentResignation.DTO.View_Models;
using System.Collections.Generic;

namespace OneTalentResignation.DAL.ResignationProcess.Interface
{
    /// <summary>
    /// Interface       : IEmployeeResignationDAL
    /// Author          : Yaseen Agwan
    /// Creation Date   : 17 October 2019
    /// Purpose         : To implement Dependency Injection
    /// </summary>
    public interface IEmployeeResignationDAL
    {
        void Apply(Resignation resignationDetail, List<ConcernEmployee> concernEmployee);

        void ResignationRevoke(Resignation revoke);

        List<EmployeeResignationViewModel> GetResignationList(int employeeId);

        List<ConcenEmployeeViewModel> GetEmployeeList();

        int GetEmployeeId(string subjectId);

        int GetEmployeeResignationId(string subjectId);

        bool IsEmployeeRegistered(int employeeId);
    }
}
