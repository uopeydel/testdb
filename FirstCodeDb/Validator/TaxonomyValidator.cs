using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstCodeDb.Validator
{
    public class TaxonomyValidator : AbstractValidator<Taxonomy>
    {
        public TaxonomyValidator()
        {

            RuleFor(r => r.Key).NotEmpty().NotNull().MinimumLength(2).MaximumLength(10);
        }
    }
}
