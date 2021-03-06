﻿using FagdagCqrs.Backend.Bootstrapping;
using FagdagCqrs.Database.Data;
using FagdagCqrs.Tests.Bdd;
using Nancy.Testing;
using NUnit.Framework;

namespace FagdagCqrs.Tests.RestApi
{
    public abstract class RestApiBddTestBase : BddTestBase
    {
        private readonly Browser _browser;

        protected RestApiBddTestBase()
        {
            _browser = new Browser(new Bootstrapper());
        }

        public Browser Browser
        {
            get { return _browser; }
        }

        [SetUp]
        public override void SetUp()
        {
            TheDatabase.Instance().Drop();
            Given();
            When();
        }
    }
}