import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { getVendorEvents, getClientEvents } from '../services/api';

function Dashboard() {
  const [events, setEvents] = useState([]);
  const [error, setError] = useState('');
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

  const handleLogout = () => {
    localStorage.removeItem('user');
    navigate('/');
  };

  return (
    <div className="max-w-4xl mx-auto mt-10 p-6">
      <div className="flex justify-between items-center mb-6">
        <h2 className="text-2xl font-bold">
          {user?.role === 'Vendor' ? 'Your Created Events' : 'Your Booked Events'}
        </h2>
        <button
          onClick={handleLogout}
          className="bg-red-600 text-white px-4 py-2 rounded hover:bg-red-700 transition"
        >
          Logout
        </button>
      </div>
      {error && <p className="text-red-500 mb-4">{error}</p>}
      {events.length === 0 ? (
        <p className="text-gray-600">No events found</p>
      ) : (
        <div className="grid gap-6">
          {events.map((event) => (
            <div
              key={event.id}
              className="p-4 bg-white rounded-lg shadow-md hover:shadow-lg transition"
            >
              <h3 className="text-xl font-semibold">{event.title}</h3>
              <p className="text-gray-600">{event.description}</p>
              <p className="text-gray-500">
                {new Date(event.date).toLocaleString()}
              </p>
              <p className="text-gray-500">Location: {event.location}</p>
            </div>
          ))}
        </div>
      )}
    </div>
  );
}

export default Dashboard;