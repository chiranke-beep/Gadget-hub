import React from 'react';
import { motion } from 'framer-motion';
import Image from '../../../components/AppImage';
import Icon from '../../../components/AppIcon';

const CategoryGrid = () => {
  const categories = [
    {
      id: 1,
      name: "Graphics Cards",
      description: "High-performance GPUs for gaming and professional work",
      image: "https://images.unsplash.com/photo-1591488320449-011701bb6704?ixlib=rb-4.0.3&auto=format&fit=crop&w=800&q=80",
      productCount: 45,
      icon: "Monitor",
      color: "from-blue-500 to-purple-600"
    },
    {
      id: 2,
      name: "Processors",
      description: "Latest CPUs from Intel and AMD",
      image: "https://images.unsplash.com/photo-1555617981-dac3880eac6e?ixlib=rb-4.0.3&auto=format&fit=crop&w=800&q=80",
      productCount: 32,
      icon: "Cpu",
      color: "from-emerald-500 to-teal-600"
    },
    {
      id: 3,
      name: "Gaming Keyboards",
      description: "Mechanical and wireless gaming keyboards",
      image: "https://images.unsplash.com/photo-1541140532154-b024d705b90a?ixlib=rb-4.0.3&auto=format&fit=crop&w=800&q=80",
      productCount: 28,
      icon: "Keyboard",
      color: "from-orange-500 to-red-600"
    },
    {
      id: 4,
      name: "Gaming Mice",
      description: "Precision gaming mice with RGB lighting",
      image: "https://images.unsplash.com/photo-1527814050087-3793815479db?ixlib=rb-4.0.3&auto=format&fit=crop&w=800&q=80",
      productCount: 35,
      icon: "Mouse",
      color: "from-pink-500 to-rose-600"
    },
    {
      id: 5,
      name: "Headsets",
      description: "Premium gaming and professional headsets",
      image: "https://images.unsplash.com/photo-1484704849700-f032a568e944?ixlib=rb-4.0.3&auto=format&fit=crop&w=800&q=80",
      productCount: 22,
      icon: "Headphones",
      color: "from-indigo-500 to-blue-600"
    },
    {
      id: 6,
      name: "Power Banks",
      description: "Portable charging solutions for all devices",
      image: "https://images.unsplash.com/photo-1609592806596-b43bdc3e5ec6?ixlib=rb-4.0.3&auto=format&fit=crop&w=800&q=80",
      productCount: 18,
      icon: "Battery",
      color: "from-green-500 to-emerald-600"
    },
    {
      id: 7,
      name: "Memory & Storage",
      description: "RAM, SSDs, and storage solutions",
      image: "https://images.unsplash.com/photo-1597872200969-2b65d56bd16b?ixlib=rb-4.0.3&auto=format&fit=crop&w=800&q=80",
      productCount: 41,
      icon: "HardDrive",
      color: "from-yellow-500 to-orange-600"
    },
    {
      id: 8,
      name: "Monitors",
      description: "4K, ultrawide, and gaming monitors",
      image: "https://images.unsplash.com/photo-1527443224154-c4a3942d3acf?ixlib=rb-4.0.3&auto=format&fit=crop&w=800&q=80",
      productCount: 29,
      icon: "Monitor",
      color: "from-purple-500 to-pink-600"
    }
  ];

  const containerVariants = {
    hidden: { opacity: 0 },
    visible: {
      opacity: 1,
      transition: {
        staggerChildren: 0.1
      }
    }
  };

  const itemVariants = {
    hidden: { opacity: 0, y: 20 },
    visible: {
      opacity: 1,
      y: 0,
      transition: {
        duration: 0.5
      }
    }
  };

  return (
    <section className="py-20 bg-gray-800">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <motion.div
          initial={{ opacity: 0, y: 20 }}
          whileInView={{ opacity: 1, y: 0 }}
          transition={{ duration: 0.6 }}
          viewport={{ once: true }}
          className="text-center mb-16"
        >
          <h2 className="text-4xl font-bold text-white mb-4">
            Shop by Category
          </h2>
          <p className="text-xl text-gray-300 max-w-2xl mx-auto">
            Explore our comprehensive collection of PC components and tech gadgets
          </p>
        </motion.div>

        <motion.div
          variants={containerVariants}
          initial="hidden"
          whileInView="visible"
          viewport={{ once: true }}
          className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-6"
        >
          {categories.map((category) => (
            <motion.div
              key={category.id}
              variants={itemVariants}
              whileHover={{ y: -5, scale: 1.02 }}
              className="group cursor-pointer"
              onClick={() => window.location.href = '/product-catalog'}
            >
              <div className="bg-gray-700 rounded-xl overflow-hidden shadow-lg hover:shadow-2xl transition-all duration-300">
                {/* Image Container */}
                <div className="relative h-48 overflow-hidden">
                  <Image
                    src={category.image}
                    alt={category.name}
                    className="w-full h-full object-cover group-hover:scale-110 transition-transform duration-500"
                  />
                  <div className={`absolute inset-0 bg-gradient-to-t ${category.color} opacity-60 group-hover:opacity-40 transition-opacity duration-300`}></div>
                  
                  {/* Icon Overlay */}
                  <div className="absolute top-4 right-4 w-12 h-12 bg-white/20 backdrop-blur-sm rounded-full flex items-center justify-center">
                    <Icon name={category.icon} size={24} color="white" />
                  </div>
                  
                  {/* Product Count Badge */}
                  <div className="absolute bottom-4 left-4 bg-black/50 backdrop-blur-sm text-white px-3 py-1 rounded-full text-sm font-medium">
                    {category.productCount} products
                  </div>
                </div>

                {/* Content */}
                <div className="p-6">
                  <h3 className="text-xl font-semibold text-white mb-2 group-hover:text-blue-400 transition-colors duration-200">
                    {category.name}
                  </h3>
                  <p className="text-gray-300 text-sm leading-relaxed">
                    {category.description}
                  </p>
                  
                  {/* View More Link */}
                  <div className="flex items-center mt-4 text-blue-400 group-hover:text-blue-300 transition-colors duration-200">
                    <span className="text-sm font-medium">View Products</span>
                    <Icon name="ArrowRight" size={16} className="ml-2 group-hover:translate-x-1 transition-transform duration-200" />
                  </div>
                </div>
              </div>
            </motion.div>
          ))}
        </motion.div>
      </div>
    </section>
  );
};

export default CategoryGrid;