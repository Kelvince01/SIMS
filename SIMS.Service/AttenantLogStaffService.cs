using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using SIMS.Data.Infrastructure;
using SIMS.Data.Repositories;
using SIMS.Models;

namespace SIMS.Service
{
    public class AttenantLogStaffService : IAttenantLogStaffService
    {
        private readonly IAttenantLogStaffRepository _attentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AttenantLogStaffService(IDbFactory idbFactory)
        {
            this._attentRepository = (IAttenantLogStaffRepository)new AttenantLogStaffRepository(idbFactory);
            this._unitOfWork = (IUnitOfWork)new UnitOfWork(idbFactory);
        }

        public IEnumerable<AttenantLogStaff> Gets(string name = null) => string.IsNullOrEmpty(name) ? this._attentRepository.GetAll() : this._attentRepository.GetAll().Where<AttenantLogStaff>((Func<AttenantLogStaff, bool>)(c => c.ShopID == name));

        public AttenantLogStaff Get(Decimal id) => this._attentRepository.GetById(id);

        public AttenantLogStaff GetByStaffAndDate(string cardNo, DateTime date) => this._attentRepository.GetMany((Expression<Func<AttenantLogStaff, bool>>)(m => m.CardNo == cardNo && DbFunctions.TruncateTime((DateTime?)m.InTime.Value) == DbFunctions.TruncateTime((DateTime?)date))).FirstOrDefault<AttenantLogStaff>();

        public void CreateUpdate(AttenantLogStaff model)
        {
            AttenantLogStaff attenantLogStaff = this._attentRepository.GetMany((Expression<Func<AttenantLogStaff, bool>>)(m => m.AttendentId == model.AttendentId && DbFunctions.TruncateTime(m.InTime) == DbFunctions.TruncateTime(model.InTime))).FirstOrDefault<AttenantLogStaff>();
            if (attenantLogStaff != null)
            {
                attenantLogStaff.OutTime = model.OutTime;
                attenantLogStaff.IsTransfer = "N";
            }
            else
                this._attentRepository.Add(model);
        }

        public void Update(AttenantLogStaff model) => this._attentRepository.Update(model);

        public void Remove(AttenantLogStaff model) => this._attentRepository.Delete(model);

        public void Save() => this._unitOfWork.Commit();

        public bool IsDuplicate(AttenantLogStaff model) => throw new NotImplementedException();

        public IEnumerable<AttenantLogStaff> SelectRecordByStaffCode(
          string Barcode,
          int top)
        {
            return this._attentRepository.GetMany((Expression<Func<AttenantLogStaff, bool>>)(m => m.CardNo == Barcode)).OrderByDescending<AttenantLogStaff, Decimal>((Func<AttenantLogStaff, Decimal>)(m => m.AttendentId)).Take<AttenantLogStaff>(top);
        }
    }
}
