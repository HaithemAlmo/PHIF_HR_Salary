using Almotkaml.HR.Domain;
using Almotkaml.Repository;
using System.Collections.Generic;

namespace Almotkaml.HR.Repository
{
    public interface IDevelopmentTypeARepository : IRepository<DevelopmentTypeA>, ICheckNameExisted
    {
        IEnumerable<DevelopmentTypeA> DevelopmentTypeAWithTrainingType(TrainingType trainingType);
    }
}