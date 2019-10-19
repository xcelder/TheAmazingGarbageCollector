import sys
import ephem
import json
import os

class SizeObject:
    SMALL = 0
    MEDIUM = 1
    LARGE = 2

    def __init__(self, norad_id, size):
        self.id = int(norad_id)
        self.size = self.get_size(size)
        
    def get_size(self, size):
        if size == 'SMALL':
            return self.SMALL
        elif size == 'MEDIUM':
            return self.MEDIUM
        elif size == 'LARGE':
            return self.LARGE
        else:
            return self.SMALL

class DebObject:

    def __init__(self, name, line1, line2, sizes_map):

        tle_rec = ephem.readtle(name, line1, line2)
        tle_rec.compute()
        ecliptic_obj_pos1 = ephem.Ecliptic(tle_rec)
        ecliptic_obj_pos2 = ephem.Ecliptic(tle_rec, epoch=0)

        name = tle_rec.name[2:]

        lat1 = ecliptic_obj_pos1.lat
        lon1 = ecliptic_obj_pos1.lon

        lat2 = ecliptic_obj_pos2.lat
        lon2 = ecliptic_obj_pos2.lon

        alt = tle_rec.elevation / 1000

        revs_per_day = tle_rec._n

        size = sizes_map.get(tle_rec.catalog_number, SizeObject.SMALL)

        self.name = name
        self.lat1 = lat1
        self.lon1 = lon1
        self.lat2 = lat2
        self.lon2 = lon2
        self.alt = alt
        self.revs_per_day = revs_per_day
        self.size = size


class Parser:

    def __init__(self, data_file_name, size_file_name):
        self.data_file = open(data_file_name, 'r')
        self.deb_objects = []
        self.sizes_map = self.load_size_data_map(size_file_name)

    @staticmethod
    def load_size_data_map(size_file_name):
        size_file = open(size_file_name, 'r')
        sizes_map = dict()

        while True:
            element = size_file.readline().strip()

            if not element:
                break

            splitted_element = element.split(' ')

            if len(splitted_element) == 2:
                norad_id, size = splitted_element
                size_obj = SizeObject(norad_id, size)
                sizes_map.update({size_obj.id: size_obj.size})

        size_file.close()
        return sizes_map

    def write_file(self):
        final_path = "%s/../The Amazing Garbage Collector/Assets/Data/GarbageData.json" % os.getcwd()
        output = open(final_path, "w+")

        output_json = dict(debris=self.deb_objects)

        deb_objs_json = json.dumps(output_json)

        output.write(deb_objs_json)
        output.close()

        print("Created file: %s" % final_path)

    def parse(self):
        while True:
            name = self.data_file.readline()
            if not name:
                break
            line1 = self.data_file.readline() 
            line2 = self.data_file.readline()

            if ' DEB' in name:
                deb_object = DebObject(name, line1, line2, self.sizes_map)
                self.deb_objects.append(deb_object.__dict__)

            
        self.data_file.close()

        self.write_file()

    
if __name__ == "__main__":
    data_file_name = "%s/3le.txt" % os.getcwd()
    size_file_name = "%s/size_data.txt" % os.getcwd()
    Parser(data_file_name=data_file_name, size_file_name=size_file_name).parse()

