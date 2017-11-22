import java.sql.*;

/*
 * Josh Gribbon
 */

public class example {
    // JDBC driver name and database URL
    static final String JDBC_DRIVER = "com.mysql.jdbc.Driver";
    static final String DB_URL = "jdbc:mysql://localhost/test";

    //  Database credentials
    static final String USER = "root";
    //the user name; You can change it to your username (by default it is root).
    static final String PASS = "root";
    //the password; You can change it to your password (the one you used in MySQL server configuration).

    public static void main(String[] args) {
        Connection conn = null;
        Statement stmt = null;
        try{
            //Register JDBC driver
            Class.forName("com.mysql.jdbc.Driver");

            //Open a connection to database
            System.out.println("Connecting to database...");
            conn = DriverManager.getConnection(DB_URL, USER, PASS);

            System.out.println("Creating database...");
            stmt = conn.createStatement();

            //drop db if it still exists
            String sql = "use BoatRental";
            stmt.executeUpdate("drop database if exists BoatRental");

            //create the db
            sql = "CREATE DATABASE BoatRental";
            stmt.executeUpdate(sql);
            System.out.println("Database created successfully...\n");

            //Use SQL to select the database;
            sql = "use BoatRental";
            stmt.executeUpdate(sql);
            
            sql = "create table Sailor(" +
            		"sid integer not null primary key," +
            		"sname varchar(20),"+
            		"rating float,"+
            		"age integer)";
            stmt.executeUpdate(sql);
            
            sql = "create table Boats("+
            		"bid integer not null primary key,"+
            		"bname varchar(40),"+
            		"color varchar(40))";
            stmt.executeUpdate(sql);
            
            sql = "create table Reserves("+
            		"sid integer not null,"+
            		"bid integer not null,"+
            		"day date not null,"+
            		"primary key(sid, bid, day),"+
            		"foreign key (sid) references Sailer(sid),"+
            		"foreign key (bid) references Boats(bid))";
            stmt.executeUpdate(sql);
            
            sql = "insert into Sailor values " +
            		"(22, 'Dustin', 7, 45),"+
            		"(29, 'Brutus', 1, 33),"+
            		"(31, 'Lubber', 8, 55),"+
            		"(32, 'Andy',   8, 26),"+
            		"(58, 'Rusty',  10, 35),"+
            		"(64, 'Horatio', 7, 35),"+
            		"(71, 'Zorba',  20, 18),"+
            		"(74, 'Horatio', 9, 35)";
            stmt.executeUpdate(sql);
            
            sql = "insert into Boats values " +
            		"(101, 'Interlake', 'Blue'),"+
            		"(102, 'Interlake', 'Red'),"+
            		"(103, 'Clipper', 'Green'),"+
            		"(104, 'Marine', 'Red')";
            stmt.executeUpdate(sql);
            
            sql = "insert into Reserves values "+
					"(22, 101, '10/10/08'),"+
					"(22, 102, '10/10/08'),"+
					"(22, 103, '10/8/09'),"+
					"(22, 104, '10/9/09'),"+
					"(31, 102, '11/10/08'),"+
					"(31, 103, '11/6/08'),"+
					"(31, 104, '11/12/08'),"+
					"(64, 101, '9/5/08'),"+
					"(64, 102, '9/8/08'),"+
					"(74, 103, '9/8/08')";
            stmt.executeUpdate(sql);
            
            try {
	            Statement s = conn.createStatement ();
	            s.executeQuery ("select * from Sailor");
	            ResultSet rs = s.getResultSet ();
	            int count = 0;
	            while (rs.next ())
	            {
	                int sid = rs.getInt ("sid");
	                String sname = rs.getString ("sname");
	                Float rating = rs.getFloat("rating");
	                int age = rs.getInt ("age");
	                System.out.println (
	                    "Sailor id = " + sid
	                    + ", name = " + sname
	                    + ", rating = " + rating
	                    + ", age = " + age);
	                ++count;
	            }
	            rs.close ();
	            s.close ();
	            System.out.println (count + " rows were retrieved\n");
            }catch(SQLException e) {
                System.err.println ("Error message: " + e.getMessage ());
                System.err.println ("Error number: " + e.getErrorCode ());
            }

        }catch(SQLException se){
            //Handle errors for JDBC
            se.printStackTrace();
        }catch(Exception e){
            //Handle errors for Class.forName
            e.printStackTrace();
        }finally{
            //finally block used to close resources
            try{
                if(stmt!=null)
                stmt.close();
            }catch(SQLException se2){
            }// nothing we can do
            try{
                if(conn!=null)
                conn.close();
            }catch(SQLException se){
                se.printStackTrace();
            }//end finally try
        }//end try
        System.out.println("Goodbye!");
    }//end main
}//end JDBCExample
