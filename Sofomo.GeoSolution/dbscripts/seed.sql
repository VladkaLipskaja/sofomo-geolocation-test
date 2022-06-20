\connect sofomo-geosolution

-- Table: public.geolocation

DROP TABLE IF EXISTS public.geolocation;

CREATE TABLE IF NOT EXISTS public.geolocation
(
    ip character varying COLLATE pg_catalog."default" NOT NULL,
    ip_type character varying COLLATE pg_catalog."default",
    continent_code character varying COLLATE pg_catalog."default",
    continent_name character varying COLLATE pg_catalog."default",
    country_code character varying COLLATE pg_catalog."default",
    country_name character varying COLLATE pg_catalog."default",
    region_code character varying COLLATE pg_catalog."default",
    region_name character varying COLLATE pg_catalog."default",
    city character varying COLLATE pg_catalog."default",
    zip character varying COLLATE pg_catalog."default",
    latitude numeric,
    longitude numeric,
    CONSTRAINT geolocation_pkey PRIMARY KEY (ip)
    )
    TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.geolocation
    OWNER to postgres;