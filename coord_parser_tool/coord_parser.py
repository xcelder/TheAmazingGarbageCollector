import sys
import ephem
import json
import os

class DebObject:
    def __init__(self, name, lat1, lon1, lat2, lon2, alt, revs_per_day):
        self.name = name
        self.lat1 = lat1
        self.lon1 = lon1
        self.lat2 = lat2
        self.lon2 = lon2
        self.alt = alt
        self.revs_per_day = revs_per_day


def parse_coordinates(raw_line):
    pass

def write_file(deb_objects):
    final_path = "%s/output.json" % os.getcwd()
    output = open(final_path, "w+")

    deb_objs_json = json.dumps(deb_objects)

    output.write(deb_objs_json)
    output.close()

    print("Created file: %s" % final_path)


def parser(file_name):

    deb_objects = []

    with open(file_name, 'r') as file:
        while True:
            line0 = file.readline()
            if not line0:
                break
            line1 = file.readline() 
            line2 = file.readline()

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

            deb_object = DebObject(name, lat1, lon1, lat2, lon2, alt, revs_per_day)

            deb_objects.append(deb_object.__dict__)

    write_file(deb_objects)

    
if __name__ == "__main__":
    file_name = sys.argv[1]
    parser(file_name=file_name)

