<?php
/**
 * Crystal DBAL
 *
 * An open source application for database manipulation
 *
 * @package		Crystal DBAL
 * @author		Martin Rusev
 * @link		http://crystal.martinrusev.net
 * @since		Version 0.1
 * @version     0.1
 */

// ------------------------------------------------------------------------
class Crystal_Query_Mysql_Get
{

    

    function __construct($method, $table)
    {
		
		$this->get = "SELECT * FROM" . Crystal_Helper_Mysql::add_apostrophe($table[0]);
		

    }

    public function __toString() 
	{
        return $this->get;
    }
    
    
}