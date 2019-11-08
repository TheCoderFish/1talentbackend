using System.Collections.Generic;

namespace OneTalentResignation.DAL.ResignationProcess.Interface
{

    /// <summary>
    /// Interface       : IConcernEmployees
    /// Author          : Divya Patel
    /// Creation Date   : 7 November 2019
    /// Purpose         : To use method in multiple class
    /// </summary>
    public interface IConcernEmployees
    {
        List<string> GetAllTheConcernEmployees(int resignationId);
    }
}
