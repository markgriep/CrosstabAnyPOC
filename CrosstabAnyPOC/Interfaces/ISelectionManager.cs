using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosstabAnyPOC.Interfaces
{
    internal class ISelectionManager
    {

        // variables

        // RDT object 
        // Pool of employees
        // selected employees



        // Constructor
        // Input is a RDT object.  That includes all the settings for the test
        // Hmnmmm... not sure if it would need to have other things injected.  Mabye the repository?



        // methods



        // Get the list of ALL employee IDs, department, jobcodes from the repository
        // parameters: none

        // Get the list of mappings from the DB, again maybe from the repository


        // Get the list of employee IDs by combining the list of IDs and the list of mappings
        // parameters: none



        // Get the list of force included employee IDs from the DB from the repository


        // get the list of force excluded employee IDs from the DB from the repository


        // you will now have a "pool" of employees, store this in a variable


        // populate the rest of the fields in the employee object like name, birthdate, etc.
        // will use the repository to get this information


        // Select employees from the pool based on the selection criteria
        // will have to generate a hashset of a number of random numbers based on the RDT settings


    }
}
