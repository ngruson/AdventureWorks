using Autofac.Features.Indexed;
using FluentValidation;
using System;

namespace AW.Common.Validation
{
    public class AutofacValidatorFactory : ValidatorFactoryBase
    {
        
        readonly IIndex<Type, IValidator> _validators;

        public AutofacValidatorFactory(IIndex<Type, IValidator> validators)
        {
            _validators = validators;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            return _validators[validatorType];
        }
    }
}