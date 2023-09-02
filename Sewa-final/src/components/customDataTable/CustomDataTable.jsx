import React from 'react'
import DataTable from 'react-data-table-component'

const CustomDataTable = ({ title, data, loading, columns, totalRows, onChange }) => {

    
    return (
        <div>     
            <DataTable
                title={title}
                columns={columns}
                data={data}
                progressPending={loading}
                pagination
                paginationServer
                paginationTotalRows={totalRows}
                paginationPerPage={5}             
                paginationComponentOptions={{
                    noRowsPerPage: true
                }}
                onChangePage={onChange}
            />
        </div>
    )
}

export default CustomDataTable
