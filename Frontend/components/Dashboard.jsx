import { useEffect, useState } from 'react'; import { useNavigate } from 'react-router-dom'; import { getVendorEvents, getClientEvents } from '../services/api';

 function Dashboard() {
   const [events, setEvents] = useState([]);
   const [error, setError] = useState('');
   const [isDark, setIsDark] = useState(false);
   const navigate = useNavigate();
   const user = JSON.parse(localStorage.getItem('user'));

   useEffect(() => {
     if (!user) {
       navigate('/');
       return;
     }

     const fetchEvents = async () => {
       try {
         let response;
         if (user.role === 'Vendor') {
           response = await getVendorEvents(user.id);
         } else {
           response = await getClientEvents(user.id);
         }
         setEvents(response.data);
       } catch (err) {
         setError('Failed to load events');
       }
     };

     fetchEvents();
   }, [user, navigate]);

   useEffect(() => {
     if (isDark) {
       document.documentElement.classList.add('dark');
     } else {
       document.documentElement.classList.remove('dark');
     }
   }, [isDark]);

   const handleLogout = () => {
     localStorage.removeItem('user');
     navigate('/');
   };

   return (
     <div className="min-h-screen bg-gradient-to-br from-gray-100 to-gray-300 dark:from-gray-800 dark:to-gray-900 transition-colors duration-300 p-6">
       <header className="sticky top-0 bg-white/90 dark:bg-gray-800/90 shadow-md py-4 mb-6 z-10 backdrop-blur-sm">
         <div className="max-w-6xl mx-auto flex justify-between items-center">
           <h2 className="text-2xl font-bold text-gray-800 dark:text-white">
             {user?.role === 'Vendor' ? 'Your Created Events' : 'Your Booked Events'}
           </h2>
           <div className="flex items-center space-x-4">
             <label className="dark-mode-toggle">
               <input
                 type="checkbox"
                 className="hidden"
                 checked={isDark}
                 onChange={() => setIsDark(!isDark)}
               />
               <span></span>
             </label>
             <button onClick={handleLogout} className="btn-secondary">
               Logout
             </button>
           </div>
         </div>
       </header>
       <div className="max-w-6xl mx-auto">
         {error && <p className="text-red-500 mb-6">{error}</p>}
         {events.length === 0 ? (
           <p className="text-gray-600 dark:text-gray-300">No events found</p>
         ) : (
           <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
             {events.map((event) => (
               <div key={event.id} className="event-card">
                 <div className="p-6">
                   <h3 className="text-xl font-semibold text-gray-800 dark:text-white">
                     {event.title}
                   </h3>
                   <p className="text-gray-600 dark:text-gray-300 mt-2">
                     {event.description}
                   </p>
                   <p className="text-gray-500 dark:text-gray-400 mt-2">
                     {new Date(event.date).toLocaleString()}
                   </p>
                   <p className="text-gray-500 dark:text-gray-400 mt-1">
                     Location: {event.location}
                   </p>
                 </div>
               </div>
             ))}
           </div>
         )}
       </div>
     </div>
   );
 }

 export default Dashboard;