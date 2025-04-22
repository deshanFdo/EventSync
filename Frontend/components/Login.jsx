import { useState } from 'react'; import { useNavigate } from 'react-router-dom'; import { login } from '../services/api';

 function Login() {
   const [name, setName] = useState('');
   const [password, setPassword] = useState('');
   const [error, setError] = useState('');
   const navigate = useNavigate();

   const handleSubmit = async (e) => {
     e.preventDefault();
     try {
       const response = await login(name, password);
       const user = response.data;
       localStorage.setItem('user', JSON.stringify(user));
       navigate('/dashboard');
     } catch (err) {
       setError('Invalid name or password');
     }
   };

   return (
     <div className="min-h-screen flex items-center justify-center bg-gradient-to-br from-gray-100 to-gray-300 dark:from-gray-800 dark:to-gray-900 transition-colors duration-300 p-4">
       <div className="glass-card p-8 max-w-sm w-full">
         <h2 className="text-3xl font-bold text-gray-800 dark:text-white text-center mb-6">Login</h2>
         {error && <p className="text-red-500 text-center mb-4">{error}</p>}
         <form onSubmit={handleSubmit}>
           <div className="mb-4">
             <label className="block text-gray-700 dark:text-gray-200 mb-2">Name</label>
             <input
               type="text"
               value={name}
               onChange={(e) => setName(e.target.value)}
               className="w-full p-3 bg-gray-100 dark:bg-gray-700 text-gray-800 dark:text-white rounded-lg"
               required
             />
           </div>
           <div className="mb-6">
             <label className="block text-gray-700 dark:text-gray-200 mb-2">Password</label>
             <input
               type="password"
               value={password}
               onChange={(e) => setPassword(e.target.value)}
               className="w-full p-3 bg-gray-100 dark:bg-gray-700 text-gray-800 dark:text-white rounded-lg"
               required
             />
           </div>
           <button type="submit" className="btn-primary w-full">
             Login
           </button>
         </form>
       </div>
     </div>
   );
 }

 export default Login;