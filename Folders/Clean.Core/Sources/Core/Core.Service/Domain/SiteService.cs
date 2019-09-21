﻿using Core.Infrastructure.Interfaces.DAL;
using $safeprojectname$.Interfaces;
using Core.Shared.DTO;
using System;
using System.Collections.Generic;

namespace $safeprojectname$.Domain
{
    public class SiteService : ISiteService
    {
        private readonly ISiteRepository _siteRepository;

        public SiteService
        (
            ISiteRepository siteRepository
        )
        {
            _siteRepository = siteRepository;
        }

        public IEnumerable<SiteDto> GetAll()
        {
            return _siteRepository.GetAll();
        }

        public bool CreateSite(SiteDto site)
        {
            return _siteRepository.Add(site);
        }

        public bool DeleteSite(Guid id)
        {
            return _siteRepository.Delete(id);
        }

        public SiteDto GetSite(Guid id)
        {
            return _siteRepository.GetById(id);
        }

        public SiteDto GetSiteByCulture(string culture)
        {
            return _siteRepository.GetByCulture(culture);
        }

        public SiteDto GetSiteByDomain(string domain)
        {
            return _siteRepository.GetByDomain(domain);
        }

        public IEnumerable<SiteDto> GetSitesPaged(int pageNumber, int pageSize)
        {
            return _siteRepository.GetPaged(pageNumber, pageSize);
        }

        public bool UpdateSite(SiteDto site)
        {
            return _siteRepository.Update(site);
        }
    }
}
