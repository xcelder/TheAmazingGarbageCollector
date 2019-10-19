import sys
import ephem
import json
import os

class DebObject:
    def __init__(self, name, lat1, lon1, lat2, lon2, alt, revs_per_day, size):
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
        self.data_file_name = data_file_name
        self.size_file_name = size_file_name

        self.data_file = open(self.data_file_name, 'r')
        self.size_file = open(self.size_file_name, 'r')
        self.deb_objects = []
        self.size_map = dict()

        self.load_size_data_map()

    def load_size_data_map(self):
        while True:
            element = self.size_file.readline().strip()

            if not element:
                break

            splitted_element = element.split(' ')

            if len(splitted_element) < 2:
                continue

            norad_id, size = splitted_element

            self.size_map.update({int(norad_id): size})
            

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
            line0 = self.data_file.readline()
            if not line0:
                break
            line1 = self.data_file.readline() 
            line2 = self.data_file.readline()

            if ' DEB' not in line0:
                continue

            tle_rec = ephem.readtle(line0, line1, line2)
            tle_rec.compute()
            ecliptic_obj_pos1 = ephem.Ecliptic(tle_rec)
            ecliptic_obj_pos2 = ephem.Ecliptic(tle_rec, epoch=0)

            name = line0[2:].rstrip()

            lat1 = ecliptic_obj_pos1.lat
            lon1 = ecliptic_obj_pos1.lon

            lat2 = ecliptic_obj_pos2.lat
            lon2 = ecliptic_obj_pos2.lon

            alt = tle_rec.elevation / 1000

            revs_per_day = tle_rec._n

            size = self.size_map.get(tle_rec.catalog_number)

            deb_object = DebObject(name, lat1, lon1, lat2, lon2, alt, revs_per_day, size)

            self.deb_objects.append(deb_object.__dict__)
            
        self.data_file.close()

        self.write_file()

    
if __name__ == "__main__":
    data_file_name = "%s/3le.txt" % os.getcwd()
    size_file_name = "%s/size_data.txt" % os.getcwd()
    Parser(data_file_name=data_file_name, size_file_name=size_file_name).parse()

