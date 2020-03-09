<script>
  import { onMount} from 'svelte'
  import { asDate} from '../../Shared/Actions'

  export let mol = null;

  let data = []
  
  onMount(async () => {
    let q = await fetch(`/api/mols/${mol.MOL_ID}/hist`);
    data = await q.json()
  })
  
  
</script>





  <table class="table table-sm">
    <tbody>
        {#each data as item}
            <tr>
                <td>{item.D_FROM}</td>
                <td>
                    <input  use:asDate bind:value={item.D_FROM} class="form-control">
                </td>
                <td>{item.POST_NAME}</td>
                <td>{item.DEPT_NAME}</td>
            </tr>
        {/each}
    </tbody>
  </table>

