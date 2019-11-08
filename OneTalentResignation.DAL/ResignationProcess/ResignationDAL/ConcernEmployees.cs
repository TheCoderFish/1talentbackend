using OneTalentResignation.DAL.Context;
using OneTalentResignation.DAL.ResignationProcess.Interface;
using System.Collections.Generic;
using System.Linq;

namespace OneTalentResignation.DAL.ResignationProcess.ResignationDAL
{
    public class ConcernEmployees : IConcernEmployees
    {
        private readonly ResignationContext _resignationcontext;
        /// <summary>
        /// Constructor   : ConcernEmployees
        /// Author        : Divya Patel
        /// Creation Date : 7 November 2019
        /// Purpose       : To resolve the dependency
        /// </summary>
        /// <param name="resignationcontext">ResignationContext</param>
        public ConcernEmployees(ResignationContext resignationcontext)
        {
            _resignationcontext = resignationcontext;
        }

        /// <summary>
        ///  Method Name    : GetAllTheConcernEmployees
        /// Author          : Divya Patel
        /// Creation Date   : 7 Novmber 2019
        /// Purpose         : Get List of Concern Employee
        /// </summary>
        /// <param name="resignationId">ResignationId</param>
        /// <returns></returns>
        public List<string> GetAllTheConcernEmployees(int resignationId)
        {
            var concernsPersons = (from employees in _resignationcontext.Employees
                                     join concernemployee in _resignationcontext.ConcernEmployees
                                     on employees.EmployeeId equals concernemployee.EmployeeId
                                     where concernemployee.ResignationId == resignationId
                                    select employees.FirstName + " " + employees.LastName).ToList();

            return concernsPersons;
        }
    }
}
