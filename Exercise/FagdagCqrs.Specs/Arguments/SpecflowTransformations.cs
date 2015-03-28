using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace FagdagCqrs.Specs.Arguments
{
    [Binding]
	public class SpecflowTransformations
	{
		
							    
		[StepArgumentTransformation]
        public IEnumerable<RomReservasjon> RomReservasjonEnumerable(Table table)
        {
			table.ValidateTransformation<RomReservasjon>();
            return table.CreateSet<RomReservasjon>();
        }

	    [StepArgumentTransformation]
        public IList<RomReservasjon> RomReservasjonList(Table table)
        {
			table.ValidateTransformation<RomReservasjon>();
            return table.CreateSet<RomReservasjon>().ToList();
        }

        [StepArgumentTransformation]
        public RomReservasjon RomReservasjon(Table table)
        {
            table.ValidateTransformation<RomReservasjon>();
            return table.CreateInstance<RomReservasjon>();
        }
		
							    
		[StepArgumentTransformation]
        public IEnumerable<RomType> RomTypeEnumerable(Table table)
        {
			table.ValidateTransformation<RomType>();
            return table.CreateSet<RomType>();
        }

	    [StepArgumentTransformation]
        public IList<RomType> RomTypeList(Table table)
        {
			table.ValidateTransformation<RomType>();
            return table.CreateSet<RomType>().ToList();
        }

        [StepArgumentTransformation]
        public RomType RomType(Table table)
        {
            table.ValidateTransformation<RomType>();
            return table.CreateInstance<RomType>();
        }
		
					
							}

}