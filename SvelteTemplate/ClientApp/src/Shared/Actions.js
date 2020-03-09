import moment from 'moment'

export function asDate(node, format) {
    
 

    if (node.nodeName==='INPUT') {
        
        node.type = "date"
        
        function onChange(e) {
            let value = e.target.value
            //node.value = moment(value).format('YYYY-MM-DDT00:00:00')
            console.log(value)
            node.value = moment(value).format('YYYY-MM-DDT00:00:00')
        
        }

        node.addEventListener('input', onChange )

    } else {

        let value = node.innerHTML
        node.innerHTML = moment(value).format(format || 'DD.MM.YYYY')

    }

    return {
		destroy() {

            console.log('destroy')
        }
	};
}