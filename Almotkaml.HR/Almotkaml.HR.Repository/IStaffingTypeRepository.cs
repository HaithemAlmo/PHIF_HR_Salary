﻿using Almotkaml.HR.Domain;
using Almotkaml.Repository;

namespace Almotkaml.HR.Repository
{
    public interface IStaffingTypeRepository : IRepository<StaffingType>, ICheckNameExisted
    {
    }
}