-- This script was generated by the ERD tool in pgAdmin 4.
-- Please log an issue at https://redmine.postgresql.org/projects/pgadmin4/issues/new if you find any bugs, including reproduction steps.
BEGIN;


CREATE TABLE IF NOT EXISTS public.circlegeofence
(
    id bigint NOT NULL DEFAULT nextval('circlegeofence_id_seq'::regclass),
    geofenceid bigint,
    radius bigint,
    latitude real,
    longitude real,
    CONSTRAINT circlegeofence_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
);

CREATE TABLE IF NOT EXISTS public.driver
(
    driverid bigint NOT NULL DEFAULT nextval('driver_driverid_seq'::regclass),
    drivername character varying COLLATE pg_catalog."default",
    phonenumber bigint,
    CONSTRAINT driver_pkey PRIMARY KEY (driverid)
)
WITH (
    OIDS = FALSE
);

CREATE TABLE IF NOT EXISTS public.geofences
(
    geofenceid bigint NOT NULL DEFAULT nextval('geofences_geofenceid_seq'::regclass),
    geofencetype character varying COLLATE pg_catalog."default",
    addeddate bigint,
    strockcolor character varying COLLATE pg_catalog."default",
    strockopacity real,
    strockweight real,
    fillcolor character varying COLLATE pg_catalog."default",
    fillopacity real,
    CONSTRAINT geofences_pkey PRIMARY KEY (geofenceid)
)
WITH (
    OIDS = FALSE
);

CREATE TABLE IF NOT EXISTS public.polygongeofence
(
    id bigint NOT NULL DEFAULT nextval('polygongeofence_id_seq'::regclass),
    geofenceid bigint,
    latitude real,
    longitude real,
    CONSTRAINT polygongeofence_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
);

CREATE TABLE IF NOT EXISTS public.rectanglegeofence
(
    id bigint NOT NULL DEFAULT nextval('rectanglegeofence_id_seq'::regclass),
    geofenceid bigint,
    north real,
    east real,
    west real,
    south real,
    CONSTRAINT rectanglegeofence_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
);

CREATE TABLE IF NOT EXISTS public.routehistory
(
    routehistoryid bigint NOT NULL DEFAULT nextval('routehistory_routehistoryid_seq'::regclass),
    vehicleid bigint,
    vehicledirection integer,
    status character(1) COLLATE pg_catalog."default",
    vehiclespeed character varying COLLATE pg_catalog."default",
    epoch bigint,
    address character varying COLLATE pg_catalog."default",
    latitude real,
    longitude real,
    CONSTRAINT routehistory_pkey PRIMARY KEY (routehistoryid)
)
WITH (
    OIDS = FALSE
);

CREATE TABLE IF NOT EXISTS public.vehicles
(
    vehicleid bigint NOT NULL DEFAULT nextval('vehicles_vehicleid_seq'::regclass),
    vehiclenumber bigint,
    vehicletype character varying COLLATE pg_catalog."default",
    CONSTRAINT vehicles_pkey PRIMARY KEY (vehicleid)
)
WITH (
    OIDS = FALSE
);

CREATE TABLE IF NOT EXISTS public.vehiclesinformations
(
    id bigint NOT NULL DEFAULT nextval('vehiclesinformations_id_seq'::regclass),
    vehicleid bigint,
    driverid bigint,
    vehiclemake character varying COLLATE pg_catalog."default",
    vehiclemodel character varying COLLATE pg_catalog."default",
    purchasedate bigint,
    CONSTRAINT vehiclesinformations_pkey PRIMARY KEY (id),
    CONSTRAINT vehicleid_unique UNIQUE (vehicleid)
)
WITH (
    OIDS = FALSE
);

ALTER TABLE IF EXISTS public.circlegeofence
    ADD CONSTRAINT circlegeofence_geofenceid_fkey FOREIGN KEY (geofenceid)
    REFERENCES public.geofences (geofenceid) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;


ALTER TABLE IF EXISTS public.polygongeofence
    ADD CONSTRAINT polygongeofence_geofenceid_fkey FOREIGN KEY (geofenceid)
    REFERENCES public.geofences (geofenceid) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;


ALTER TABLE IF EXISTS public.rectanglegeofence
    ADD CONSTRAINT rectanglegeofence_geofenceid_fkey FOREIGN KEY (geofenceid)
    REFERENCES public.geofences (geofenceid) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;


ALTER TABLE IF EXISTS public.routehistory
    ADD CONSTRAINT routehistory_vehicleid_fkey FOREIGN KEY (vehicleid)
    REFERENCES public.vehicles (vehicleid) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;


ALTER TABLE IF EXISTS public.vehiclesinformations
    ADD CONSTRAINT vehiclesinformations_driverid_fkey FOREIGN KEY (driverid)
    REFERENCES public.driver (driverid) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE SET NULL;


ALTER TABLE IF EXISTS public.vehiclesinformations
    ADD CONSTRAINT vehiclesinformations_vehicleid_fkey FOREIGN KEY (vehicleid)
    REFERENCES public.vehicles (vehicleid) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;
CREATE INDEX IF NOT EXISTS vehicleid_unique
    ON public.vehiclesinformations(vehicleid);

END;