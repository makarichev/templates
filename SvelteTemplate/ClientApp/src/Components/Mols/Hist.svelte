<script>
  import { onMount} from 'svelte'
  import { asLoader} from '../../Shared/Actions.js'
  

  export let mol = null;

  let data = []
  let loading;
  
  onMount(async () => {
    loading = true
    let q = await fetch(`/api/mols/${mol.MOL_ID}/hist`);
    data = await q.json()
    console.log(data)
    loading = false
  })
  
  
</script>
  
  <h5 use:asLoader={loading}>История назначений</h5>

  <table class="table table-sm">
    <tbody>
        {#each data as item}
            <tr>
                <td>{item.D_FROM}</td>
                <td>
                    
                </td>
                <td>{item.POST_NAME}</td>
                <td>{item.DEPT_NAME}</td>
            </tr>
        {/each}
    </tbody>
  </table>

