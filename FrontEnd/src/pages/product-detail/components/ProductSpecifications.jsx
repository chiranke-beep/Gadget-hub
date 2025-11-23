import React, { useState } from 'react';
import Icon from '../../../components/AppIcon';

const ProductSpecifications = ({ specifications }) => {
  const [expandedSections, setExpandedSections] = useState({});

  if (!specifications || typeof specifications !== 'object' || Object.keys(specifications).length === 0) {
    return null;
  }

  const toggleSection = (sectionKey) => {
    setExpandedSections(prev => ({
      ...prev,
      [sectionKey]: !prev[sectionKey]
    }));
  };

  return (
    <div className="bg-card rounded-lg p-6">
      <h2 className="text-xl font-bold text-foreground mb-6 flex items-center">
        <Icon name="FileText" size={20} className="mr-2" />
        Detailed Specifications
      </h2>

      <div className="space-y-4">
        {Object.entries(specifications).map(([sectionKey, sectionData]) => (
          <div key={sectionKey} className="border border-border rounded-lg">
            <button
              onClick={() => toggleSection(sectionKey)}
              className="w-full flex items-center justify-between p-4 text-left hover:bg-muted transition-colors duration-150"
            >
              <h3 className="text-lg font-semibold text-foreground">
                {sectionData.title}
              </h3>
              <Icon
                name="ChevronDown"
                size={20}
                className={`transform transition-transform duration-200 ${
                  expandedSections[sectionKey] ? 'rotate-180' : ''
                }`}
              />
            </button>

            {expandedSections[sectionKey] && (
              <div className="px-4 pb-4 border-t border-border">
                <div className="grid grid-cols-1 md:grid-cols-2 gap-4 mt-4">
                  {sectionData.specs.map((spec, index) => (
                    <div key={index} className="flex justify-between items-start py-2">
                      <span className="text-muted-foreground font-medium">
                        {spec.label}:
                      </span>
                      <span className="text-foreground text-right ml-4">
                        {spec.value}
                      </span>
                    </div>
                  ))}
                </div>
              </div>
            )}
          </div>
        ))}
      </div>

      {/* Additional Information */}
      <div className="mt-6 p-4 bg-muted rounded-lg">
        <div className="flex items-start space-x-2">
          <Icon name="Info" size={16} className="text-primary mt-0.5 flex-shrink-0" />
          <div className="text-sm text-muted-foreground">
            <p className="font-medium text-foreground mb-1">Important Note:</p>
            <p>
              Specifications may vary slightly depending on the specific model and region. 
              Please contact our sales team for the most accurate specifications for your requirements.
            </p>
          </div>
        </div>
      </div>
    </div>
  );
};

export default ProductSpecifications;